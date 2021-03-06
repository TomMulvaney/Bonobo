﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Bonobo
{
    public class SpriteAnimator : MonoBehaviour 
    {
        public delegate void SpriteAnimatorEventHandler(SpriteAnimator animBehaviour, string animName);
        public event SpriteAnimatorEventHandler AnimFinished;
        
        [System.Serializable]
        public class AnimDetails
        {
            [SerializeField]
            public UIAtlas m_spriteAtlas;
            [SerializeField]
            public string m_name;
            [SerializeField]
            public string m_prefix;
            [SerializeField]
            public int m_numFrames;
            [SerializeField]
            public int m_startFrame;
            [SerializeField]
            public bool m_toLetter;
            [SerializeField]
            public float m_fps;
            [SerializeField]
            public bool m_loop;
            [SerializeField]
            public bool m_isReverse;
            [SerializeField]
            public string m_sequencedAnimation;
            [SerializeField]
            public float m_sequenceDelay;
        }

        [SerializeField]
        AnimDetails[] m_availableAnimations;
        [SerializeField]
        UISprite m_sprite;
        [SerializeField]
        string m_format = "{0:00}";
        [SerializeField]
        bool m_startOn = true;
        [SerializeField]
        private List<string> m_randomAnimationNames;
        [SerializeField]
        private Vector2 m_randomDelayRange = new Vector2(2, 5);

        int m_currentAnimation = 0;
        float m_currentT;
        int m_currentFrame;

        bool m_playAnim;

        bool m_isWaiting = false;
        string m_waitingAnimationName;

        bool m_isWaitingToPlay = false;

        void OnEnable()
        {
            if (m_isWaitingToPlay)
            {
                StartCoroutine("PlayRandom");
            }
        }
        
        void Start()
        {
            if(m_sprite == null)
            {
                m_sprite = GetComponent<UISprite>() as UISprite;
            }

            if (string.IsNullOrEmpty(m_format))
            {
                m_format = "{0}";
            }

            m_playAnim = m_startOn;

            if (m_playAnim && m_randomAnimationNames.Count > 0)
            {
                PlayAnimation(m_randomAnimationNames[UnityEngine.Random.Range(0, m_randomAnimationNames.Count)], true);
            }
        }

        public void Stop()
        {
            StopAllCoroutines();
            m_playAnim = false;
            m_isWaitingToPlay = false;
        }

        public void StopRandom()
        {
            StopCoroutine("PlayRandom");
            m_isWaitingToPlay = false;
        }

        IEnumerator PlayRandom()
        {
            m_isWaitingToPlay = true;

            yield return new WaitForSeconds(UnityEngine.Random.Range(m_randomDelayRange.x, m_randomDelayRange.y));

            m_isWaitingToPlay = false;

            if (m_randomAnimationNames.Count > 0)
            {
                PlayAnimation(m_randomAnimationNames[UnityEngine.Random.Range(0, m_randomAnimationNames.Count)], true);
            }
        }

        public bool IsPlaying()
        {
            return m_playAnim && m_currentAnimation > -1 && m_currentAnimation < m_availableAnimations.Length;
        }

        int GetAnimIndex(string animName)
        {
            return Array.FindIndex(m_availableAnimations, x => x.m_name == animName);
        }
        
        public bool HasAnim(string animName)
        {
            return GetAnimIndex(animName) != -1;
        }
        
        public void PlayAnimation(string animName, bool waitForEnd = true)
        {
            StopAllCoroutines();

            if (waitForEnd && IsPlaying())
            {
                m_waitingAnimationName = animName;
                m_isWaiting = true;
            } 
            else
            {
                m_waitingAnimationName = "";

                int animIndex = GetAnimIndex(animName);

                if(animIndex != -1)
                {
                    m_currentAnimation = animIndex;
                    m_currentFrame = 0;

                    AnimDetails ad = m_availableAnimations[m_currentAnimation];

                    if (ad.m_spriteAtlas != null)
                    {
                        m_sprite.atlas = ad.m_spriteAtlas;
                    }
                    
                    SetSpriteName();
                    
                    m_playAnim = true;
                }
            }
        }

        IEnumerator PlaySequencedAnimation(AnimDetails ad)
        {
            m_isWaitingToPlay = true;

            yield return new WaitForSeconds(ad.m_sequenceDelay);

            m_isWaitingToPlay = false;

            PlayAnimation(ad.m_sequencedAnimation, false);
        }

        void Update () 
        {
            if (IsPlaying())
            {
                AnimDetails ad = m_availableAnimations[m_currentAnimation];

                m_currentT += Time.deltaTime * ad.m_fps;
                if (m_currentT > 1)
                {
                    m_currentT -= 1;

                    if(Mathf.Abs(m_currentFrame) < Mathf.Abs(ad.m_numFrames))
                    {
                        SetSpriteName();

                        if(ad.m_isReverse)
                        {
                            --m_currentFrame;
                        }
                        else
                        {
                            ++m_currentFrame;
                        }
                    }
                    else
                    {
                        if(!String.IsNullOrEmpty(ad.m_sequencedAnimation) && HasAnim(ad.m_sequencedAnimation))
                        {
                            m_playAnim = false;
                            StartCoroutine(PlaySequencedAnimation(ad));
                        }
                        else if(m_isWaiting)
                        {
                            m_isWaiting = false;
                            PlayAnimation(m_waitingAnimationName, false);
                        }
                        else if(ad.m_loop)
                        {
                            m_currentFrame = 0;
                        }
                        else
                        {
                            m_playAnim = false;

                            if(m_randomAnimationNames.Count > 0)
                            {
                                StartCoroutine("PlayRandom");
                            }

                            if(AnimFinished != null)
                            {
                                AnimFinished(this, ad.m_name);
                            }
                        }
                    }
                }
            }
        }
        
        void SetSpriteName()
        {
            int frame = m_currentFrame + m_availableAnimations[m_currentAnimation].m_startFrame;
            string frameString = string.Format(m_format, frame);

            if (m_availableAnimations[m_currentAnimation].m_toLetter)
            {
                frameString = ((char)('a' + frame)).ToString();
            }

            m_sprite.spriteName = m_availableAnimations [m_currentAnimation].m_prefix + frameString;
        }
    }
}
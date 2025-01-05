﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame
{

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseUI : TickBehaviour
    {
        public CanvasGroup Group;
#if UNITY_EDITOR
        void OnValidate()
        {
            Group = GetComponent<CanvasGroup>();
            Debug.Log(typeof(BaseUI).Name + " OnValidate");
        }
#endif

        protected override void Awake()
        {
            base.Awake();
            Init();
        }
        public virtual void SetInfo() { }
        public virtual void Init() { }
        [Button]
        public virtual void OnShow()
        {
            Group.alpha = 1;
            Group.blocksRaycasts = true;
            gameObject.SetActive(true);
        }
        IEnumerator IE_Show(float duration)
        {
            Group.alpha = 0;
            while (Group.alpha < 1)
            {
                Group.alpha = Mathf.MoveTowards(Group.alpha, 1, Time.unscaledDeltaTime / duration);

                yield return new WaitForEndOfFrame();
            }
            OnShow();
        }

        public virtual void Show(float duration = 0.5f)
        {
            gameObject.SetActive(true);
            if (duration > 0)
                StartCoroutine(IE_Show(duration));
            else
            {
                OnShow();
            }
        }

        IEnumerator IE_Hide()
        {
            while (Group.alpha > 0)
            {
                Group.alpha = Mathf.MoveTowards(Group.alpha, 0, Time.unscaledDeltaTime * 10);

                yield return new WaitForEndOfFrame();
            }
            OnHide();
        }
        [Button]
        public virtual void OnHide()
        {
            Group.alpha = 0;
            Group.blocksRaycasts = false;
            gameObject.SetActive(false);
        }

        public virtual void Hide()
        {
            if (!gameObject.activeSelf)
            {
                OnHide();
            }
            else
            {
                StartCoroutine(IE_Hide());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEssentials
{
    public enum UITransitionState
    {
        Opacity0,
        Opacity1,
        Translate0,
        TranslateLeft,
        TranslateLeftX,
        TranslateUp,
        TranslateUpX,
        TranslateRight,
        TranslateRightX,
        TranslateDown,
        TranslateDownX,
        Scale0,
        Scale0_5,
        Scale0_9,
        Scale1,
        Scale1_01,
        Scale1_1,
        Scale1_5,
        Scale2,
        Rotate0,
        Rotate45,
        Rotate90,
        RotateMinus45,
        RotateMinus90,
        Rotate180,
    }

    public enum UITransitionDuration
    {
        _0_05s,
        _0_1s,
        _0_25s,
        _0_5s,
        _0_75s,
        _1s,
        _1_5s,
        _2s,
        _2_5s,
        _3s,
        _5s,
        _10s
    }

    public class UIAnimation : BaseScriptComponent<VisualElement>
    {
        public UITransitionState[] InitialTransitionStates = { UITransitionState.Opacity0 };

        [Space]
        public UITransitionDuration Duration = UITransitionDuration._0_5s;
        public float Delay = 0;
        public bool FadeOut = false;

        private bool _isPlaying = false;
        private const string _transitionBaseClassName = "TransitionBase";
        private float[] _durationIndexTime = new float[]
        {
            0.05f,
            0.1f,
            0.25f,
            0.5f,
            0.75f,
            1.0f,
            1.5f,
            2.0f,
            2.5f,
            3.0f,
            5.0f,
            10.0f
        };

        private string[] _fadeOutState = new string[]
        {
            "FadeOut0_05",
            "FadeOut0_1",
            "FadeOut0_25",
            "FadeOut0_5",
            "FadeOut0_75",
            "FadeOut1",
            "FadeOut1_5",
            "FadeOut2",
            "FadeOut2_5",
            "FadeOut3",
            "FadeOut5",
            "FadeOut10",
        };

        public void Start()
        {
            if (!HasElements)
                return;

            var transitionUSS = ResourceLoader.LoadResource<StyleSheet>("UnityEssentials_USS_Transition");
            if (transitionUSS != null)
                Document.AddStyleSheet(transitionUSS);

            Play();
        }

        [Button("Play Animation")]
        public void Play()
        {
            if (FadeOut)
                Timing.RunCoroutine(PlayFadeOut(), handleGroup: "UI Animation");

            if (InitialTransitionStates.Length == 0) 
                return;

            if (_isPlaying)
                StopTransition();

            Timing.RunCoroutine(PlayTransition(), handleGroup: "UI Animation");
        }

        private IEnumerator<float> PlayFadeOut()
        {
            string fadeOutState = _fadeOutState[(int)Duration];

            IterateLinkedElements(e => e.RemoveFromClassList(fadeOutState));

            yield return Delay;

            IterateLinkedElements(e => e.AddToClassList(fadeOutState));
        }

        private IEnumerator<float> PlayTransition()
        {
            foreach (var initialState in InitialTransitionStates)
                IterateLinkedElements(e => e.AddToClassList(initialState.ToString()));

            _isPlaying = true;

            yield return Delay;

            IterateLinkedElements(e => e.AddToClassList(_transitionBaseClassName + (int)Duration));

            Timing.RunCoroutine(RemoveClassAfterDelay(), handleGroup: "UI Animation");
        }

        public void StopTransition()
        {
            if (!_isPlaying)
                return;

            Timing.KillAllCoroutines("UI Animation");

            RemoveClasses();

            _isPlaying = false;
        }

        private IEnumerator<float> RemoveClassAfterDelay()
        {
            yield return _durationIndexTime[(int)Duration];

            RemoveClasses();

            _isPlaying = false;
        }

        private void RemoveClasses()
        {
            foreach (var initialState in InitialTransitionStates)
                IterateLinkedElements(e => e.RemoveFromClassList(initialState.ToString()));

            IterateLinkedElements(e => e.RemoveFromClassList(_transitionBaseClassName + (int)Duration));
        }
    }
}
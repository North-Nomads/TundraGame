﻿using UnityEngine;

namespace Spells
{
    public class MagicElementModel
    {
        private BasicElement _elementType;

        private Sprite _elementSprite;

        private int _currentCharges;

        private int _maxCharges;

        private float _reloadTime;

        private float _currentReloadTime;

        public void ContinueReloading()
        {
            if (_currentCharges < _maxCharges)
            {
                _currentReloadTime += Time.deltaTime;
                if (_currentReloadTime > _reloadTime)
                {
                    _currentCharges++;
                    _currentReloadTime -= _reloadTime;
                }
            }
        }
    }
}
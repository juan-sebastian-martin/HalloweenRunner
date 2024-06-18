using System;
using System.ComponentModel;
using Builder;
using Component_Models.Contracts;
using UnityEngine;

namespace Components
{
    public class PlayerDeath : MonoBehaviour
    {
        private IPlayerDeathComponentModel _componentModel;
        private Animator _animator;

        private void Awake()
        {
            var builder = new PlayerDeathComponentModelBuilder();
            builder.Create();
            _componentModel = builder.GetPlayerDeathComponentModel();

            _componentModel.PropertyChanged += OnPropertyChanged;

        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_componentModel.IsDead))
            {
                if(_componentModel.IsDead)
                    ReproduceDeath();
            }
        }

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void ReproduceDeath()
        {
            _animator.SetTrigger("isDead");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Monster"))
            {
                _componentModel.PlayerDies();
            }
        }

        private void OnDestroy()
        {
            _componentModel.Dispose();
        }
    }
}
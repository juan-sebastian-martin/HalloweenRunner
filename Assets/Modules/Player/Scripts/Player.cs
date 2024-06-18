using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Modules.Player.Scripts
{
    public class Player : IPlayer
    {
        private bool _isGrounded;
        private bool _isDead;
        private bool _isAbleToJump;
        private int _currentAmountOfJumps;
        private float _speed;
        private float _currentSpeed;

        public bool IsGrounded {get { return _isGrounded; }
            set
            {
                if (value)
                {
                    _currentAmountOfJumps = 0;
                }
                
                if (value != _isGrounded)
                {
                    _isGrounded = value;
                    OnPropertyChanged(nameof(IsGrounded));
                }
                    
            }}

        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            set
            {
                if (value != _isDead)
                {
                    _isDead = value;
                    OnPropertyChanged();
                }
            }
        }

        public float CurrentSpeed
        {
            get => _currentSpeed;
            set
            {
                if (value != _currentSpeed)
                {
                    _currentSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Run(float speed)
        {
            _speed = speed;
            CurrentSpeed = _speed;
        }

        public bool Jump()
        {
            if (_isDead)
            {
                return false;
            }
            if (_isGrounded || _currentAmountOfJumps<2)
            {
                _currentAmountOfJumps++;
                _isAbleToJump = true;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void Death()
        {
            CurrentSpeed = 0;
            IsDead = true;
            
        }

        public void SetGrounded(bool isGrounded)
        {
            IsGrounded = isGrounded;
        }

        public void ReduceSpeed()
        {
            CurrentSpeed = _speed - 1f;
        }

        public void ResumeSpeed()
        {
            CurrentSpeed = _speed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
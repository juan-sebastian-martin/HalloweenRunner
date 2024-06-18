using System.ComponentModel;
using System.Runtime.CompilerServices;
using Installers;
using UnityEngine;

namespace Modules.Player.Scripts
{
    public class GroundCheck : MonoBehaviour,IGroundCheck
    {
        private bool _isPlayerGrounded;

        public LayerMask GroundLayerMask;

        private BoxCollider2D _boxCollider;
        private GroundCheckVM _groundCheckVm;
        [SerializeField] private Animator _animator;
        void Start()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _groundCheckVm = new GroundCheckVM(this, ServiceLocator.Instance.GetService<IPlayerRepository>());
        }
        void FixedUpdate()
        {
            float extraHeightText = 0.5f;
            RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, extraHeightText, GroundLayerMask);
            Color rayColor;
            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(_boxCollider.bounds.center + new Vector3(_boxCollider.bounds.extents.x, 0), Vector2.down * (_boxCollider.bounds.extents.y + extraHeightText), rayColor);
            Debug.DrawRay(_boxCollider.bounds.center - new Vector3(_boxCollider.bounds.extents.x, 0), Vector2.down * (_boxCollider.bounds.extents.y + extraHeightText), rayColor);
            Debug.DrawRay(_boxCollider.bounds.center - new Vector3(_boxCollider.bounds.extents.x, _boxCollider.bounds.extents.y + extraHeightText), Vector2.down * (_boxCollider.bounds.extents.y + extraHeightText), rayColor);

            IsPlayerGrounded = raycastHit.collider != null;

            if (IsPlayerGrounded)
            {
                _animator.SetBool("isRunning", true);
            }else{
                _animator.SetBool("isRunning", false);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsPlayerGrounded
        {
            get
            {
                return _isPlayerGrounded;
            }
            set
            {
                if (_isPlayerGrounded != value)
                {
                    _isPlayerGrounded = value;
                    OnPropertyChanged(nameof(IsPlayerGrounded));
                }
                    
            }
        }

        private void OnDestroy()
        {
            _groundCheckVm.Dispose();
        }
    }
}
using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 1)]
    public class Player : ScriptableObject {
        public int maxHealth;
        private int _health;
        
        private bool _canBullet;
        private bool _canLaser;

        public void Init() {
            _health = maxHealth;
            _canBullet = false;
            _canLaser = false;
        }

        public void ChangeHealth(int number) {
            _health += number;
        }

        public int GetHealth() {
            return _health;
        }

        public void SetShoot(int index, bool value) {
            if (index == 0) _canLaser = value;
            else _canBullet = value;
        }
        
        public bool CanShoot(int index) {
            if (index == 0) return _canLaser;
            return _canBullet;
        }

    }
}

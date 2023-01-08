using NUnit.Framework;
using UnityEngine;

namespace _Project.Scripts.Tower.Editor
{
    public class TowerTests
    {
        [Test]
        public void GIVEN_TOWER_AND_UNIT_ATTACK__SHOULD_DAMAGE()
        {
            // Given
            var tower = new AttackOneUnitTower(new TowerStats
            {
                Damage = 10,
                DelayExecution = 1f
            }, new DummyTowerView());

            var damageable = new DummyDamageable();
            tower.Add(damageable);
            
            // Act
            tower.Update(200f);
            
            
            // Should
            Assert.AreEqual(10, damageable.LastDamageAmount);
        }

        [Test]
        public void GIVEN_TOWER_AND_DEAD_UNIT__ATTACK__SHOULD_NOT_TAKE_DAMAGE()
        {
            // Given
            var tower = new AttackOneUnitTower(new TowerStats
            {
                Damage = 10,
                DelayExecution = 1f
            }, new DummyTowerView());

            var damageable = new DummyDamageable();
            damageable.IsAlive = false;
            tower.Add(damageable);
            
            // Act
            tower.Update(200f);
            
            
            // Should
            Assert.AreEqual(0, damageable.LastDamageAmount);
        }

        [Test]
        public void GIVEN_TOWER_UNIT_EXIT_TOWER__SHOULD_DAMAGE()
        {
            // Given
            var tower = new AttackOneUnitTower(new TowerStats
            {
                Damage = 10,
                DelayExecution = 1f
            }, new DummyTowerView());

            var damageable = new DummyDamageable();
            tower.Add(damageable);
            tower.Remove(damageable);
            
            // Act
            tower.Update(200f);
            
            
            // Should
            Assert.AreEqual(0, damageable.LastDamageAmount);
        }

        [Test]
        public void GIVEN_AOE_TOWER_AND_UNIT__ATTACK_IN_RANGE__SHOULD_DAMAGE()
        {
            // Given
            var tower = new AttackAoeTower(new TowerStats
            {
                Damage = 10,
                DelayExecution = 1f,
                AoeRange = 2f
            }, new DummyTowerView());

            var d1 = new DummyDamageable();
            d1.Position = Vector3.zero;
            var d2 = new DummyDamageable();
            d2.Position = Vector3.one;
            tower.Add(d1);
            tower.Add(d2);
            
            // Act
            tower.Update(200f);
            
            
            // Should
            Assert.AreEqual(10, d1.LastDamageAmount);
            Assert.AreEqual(10, d2.LastDamageAmount);
        }
        
        [Test]
        public void GIVEN_AOE_TOWER_AND_UNIT__ATTACK_IN_NOT_RANGE__SHOULD_DAMAGE_FIRST_UNIT()
        {
            // Given
            var tower = new AttackAoeTower(new TowerStats
            {
                Damage = 10,
                DelayExecution = 1f,
                AoeRange = 2f
            }, new DummyTowerView());

            var d1 = new DummyDamageable();
            d1.Position = Vector3.zero;
            var d2 = new DummyDamageable();
            d2.Position = Vector3.one * 1000f;
            tower.Add(d1);
            tower.Add(d2);
            
            // Act
            tower.Update(200f);
            
            
            // Should
            Assert.AreEqual(10, d1.LastDamageAmount);
            Assert.AreEqual(0, d2.LastDamageAmount);
        }
        
        [Test]
        public void GIVEN_AOE_SLOW_TOWER_AND_UNIT__ATTACK_IN_RANGE__SHOULD_SLOW()
        {
            // Given
            var tower = new SlowAoeTower(new TowerStats
            {
                Damage = 10,
                DelayExecution = 1f,
                AoeRange = 2f,
                SlowTime = 2f
            }, new DummyTowerView());

            var d1 = new DummyDamageable();
            d1.Position = Vector3.zero;
            tower.Add(d1);
            
            // Act
            tower.Update(200f);
            
            
            // Should
            Assert.AreEqual(2f, d1.LastSlowTime);
        }
        
        [Test]
        public void GIVEN_TOWER_AND_UNIT__ATTACK__SHOULD_LOOT_AT_UNIT()
        {
            // Given
            var view = new DummyTowerView();
            var tower = new AttackOneUnitTower(new TowerStats
            {
                Damage = 10,
                DelayExecution = 1f
            }, view);

            var damageable = new DummyDamageable();
            damageable.Position = Vector3.one * 400;
            tower.Add(damageable);
            
            // Act
            tower.Update(200f);
            
            
            // Should
            Assert.AreEqual(Vector3.one * 400, view.LookAtPos);
        }



    }


    internal class DummyTowerView : ITowerView
    {
        public Vector3 LookAtPos;
        public void LookAt(Vector3 pos)
        {
            LookAtPos = pos;
        }
    }

    internal class DummyDamageable : IDamageable
    {
        public bool IsAlive { get; set; } = true;

        public float LastDamageAmount;
        public float LastSlowTime;
        public Vector3 Position { get; set; }

        public void TakeDamage(in float amount)
        {
            LastDamageAmount = amount;
        }

        public void SlowDown(in float time)
        {
            LastSlowTime = time;
        }

    }
}
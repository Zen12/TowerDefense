using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Project.Scripts.SpawnSystems.Editor
{
    public class SpawnTest
    {
        [UnityTest]
        public IEnumerator GIVEN_1_WAVE__START_WAVESYSTEM__SHOULD_FINISH_WAVE()
        {
            // Given
            var settings = new WaveSettings
            {
                Waves = new List<Wave>
                {
                    new Wave
                    {
                        Amount = 2,
                        Delay = 0.2f,
                        Type = 0
                    }
                }
            };
            var fabric = new DummyFabric();
            var wave = new WaveController(settings, fabric, CancellationToken.None);
            
            
            // Act
            var times = 0;
            while (wave.IsFinishAllWaves == false)
            {
                wave.Update();
                yield return null;
                times++;
                if (times > 10000)
                    throw new Exception("Entered while true");
            }
            
            // Should
            Assert.IsTrue(wave.IsFinishAllWaves);
        }

    }

}

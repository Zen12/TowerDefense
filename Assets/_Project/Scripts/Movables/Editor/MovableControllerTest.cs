using NUnit.Framework;

namespace _Project.Scripts.Movables.Editor
{
    public class MovableControllerTest
    {
        [Test]
        public void GIVEN_1_MOVABLE__MOVE_TILL_END__SHOULD_REMOVE_IT()
        {
            // Given
            var listener = new DummyListener();
            var controller = new MovableController(new DummyPath(), listener);
            var movable = new DummyMovable();
            controller.RegisterMovable(movable, 1f);
            
            // Act
            controller.Update(1.1f);
            
            // Should
            Assert.AreEqual(movable, listener.Movable[0]);
        }
        
        [Test]
        public void GIVEN_1_MOVABLE__MOVE_TILL_END__SHOULD_POSITION_BE_LAST_PATH()
        {
            // Given
            var listener = new DummyListener();
            var path = new DummyPath();
            var controller = new MovableController(path, listener);
            var movable = new DummyMovable();
            controller.RegisterMovable(movable, 1f);
            
            // Act
            controller.Update(1.1f);
            
            // Should
            Assert.AreEqual(path.GetPositionFromTime(1.0f), movable.Position);
        }
        
        [Test]
        public void GIVEN_1_MOVABLE__MOVE_TILL_HALF__SHOULD_HALF_TILL_END()
        {
            // Given
            var listener = new DummyListener();
            var path = new DummyPath();
            var controller = new MovableController(path, listener);
            var movable = new DummyMovable();
            controller.RegisterMovable(movable, 1f);
            
            // Act
            controller.Update(0.5f);
            
            // Should
            Assert.AreEqual(path.GetPositionFromTime(0.5f), movable.Position);
        }
        
        [Test]
        public void GIVEN_1_MOVABLE__MOVE_TILL_HALF__SHOULD_NOT_BE_REMOVED()
        {
            // Given
            var listener = new DummyListener();
            var path = new DummyPath();
            var controller = new MovableController(path, listener);
            var movable = new DummyMovable();
            controller.RegisterMovable(movable, 1f);
            
            // Act
            controller.Update(0.5f);
            
            // Should
            Assert.AreEqual(0, listener.Movable.Count);
        }
        
        [Test]
        public void GIVEN_3_MOVABLE__MOVE_ONE_TILL_END__SHOULD_REMOVE_ONLY_ONE()
        {
            // Given
            var listener = new DummyListener();
            var path = new DummyPath();
            var controller = new MovableController(path, listener);
            controller.RegisterMovable(new DummyMovable(), 0.1f);
            controller.RegisterMovable(new DummyMovable(), 0.1f);
            controller.RegisterMovable(new DummyMovable(), 1f);
            
            // Act
            controller.Update(1.1f);
            
            // Should
            Assert.AreEqual(1, listener.Movable.Count);
        }
    }

}
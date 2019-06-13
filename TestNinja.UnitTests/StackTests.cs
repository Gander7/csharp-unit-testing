using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<string> _stack;

        [SetUp]
        public void Setup()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_WhenCalled_CountShouldIncrease()
        {
            _stack.Push("first");

            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Push_ArgIsNull_ThrowsArgNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _stack.Push(null));
        }

        [Test]
        public void Pop_PopulatedStack_ReturnsLastElement()
        {
            _stack.Push("first");
            _stack.Push("second");

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo("second"));
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Pop());
        }

        [Test]
        public void Peek_PopulatedStack_ReturnsLastElement()
        {
            _stack.Push("first");
            _stack.Push("second");

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo("second"));
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowsInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Peek());
        }
    }
}

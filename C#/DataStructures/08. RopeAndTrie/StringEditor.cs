using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace TrieRope
{
    class StringEditor : ITextEditor
    {
        private Trie<BigList<char>> usersStrings;
        private Trie<Stack<string>> usersStack;

        public StringEditor()
        {
            this.usersStrings = new Trie<BigList<char>>();
            this.usersStack = new Trie<Stack<string>>();
        }

        public void Login(string username)
        {
            this.usersStrings.Insert(username, new BigList<char>());
            this.usersStack.Insert(username, new Stack<string>());
        }

        public void Logout(string username)
        {
            this.usersStrings.Delete(username);
            this.usersStack.Delete(username);
        }

        public string Print(string username)
        {
            if (!this.usersStrings.Contains(username)) {
                return "";
            }

            
            return string.Join("", this.usersStrings.GetValue(username));
        }

        public void Prepend(string username, string str)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var stack = this.usersStack.GetValue(username);
            stack.Push(string.Join("", this.usersStrings.GetValue(username)));


            var biglist = this.usersStrings.GetValue(username);
            biglist.AddRangeToFront(str);
        }

        public void Clear(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var userHistory = this.usersStack.GetValue(username);
            userHistory.Push(string.Join("", this.usersStrings.GetValue(username)));

            this.usersStrings.Insert(username, new BigList<char>());
        }

        public void Delete(string username, int startIndex, int length)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var userHistory = this.usersStack.GetValue(username);
            userHistory.Push(string.Join("", this.usersStrings.GetValue(username)));

            var userString = this.usersStrings.GetValue(username);
            userString.RemoveRange(startIndex, length);
        }

        public void Insert(string username, int index, string str)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var userHistory = this.usersStack.GetValue(username);
            userHistory.Push(string.Join("", this.usersStrings.GetValue(username)));

            var userString = this.usersStrings.GetValue(username);
            userString.InsertRange(index, str);
        }

        public int Length(string username)
        {
            throw new NotImplementedException();
        }

        public void Substring(string username, int startIndex, int length)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var userString = this.usersStrings.GetValue(username);
            var userHistory = this.usersStack.GetValue(username);
            userHistory.Push(string.Join("", userString));

            var newUserString = new BigList<char>();
            for (int i = startIndex; i < startIndex + length; i++)
            {
                newUserString.Add(userString[i]);
            }

            this.usersStrings.Insert(username, newUserString);
        }

        public void Undo(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var userString = this.usersStrings.GetValue(username);
            var userHistory = this.usersStack.GetValue(username);
            if (userHistory.Count == 0)
            {
                return;
            }

            var lastUserString = userHistory.Pop();
            userHistory.Push(string.Join("", userString));

            this.usersStrings.Insert(username, new BigList<char>(lastUserString));
        }

        public IEnumerable<string> Users(string prefix = "")
        {
            throw new NotImplementedException();
        }
    }
}

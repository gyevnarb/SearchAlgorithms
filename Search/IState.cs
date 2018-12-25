// https://projectbalint.com/en/page/uninformed-search.html
// State.cs
//
// An arbitrary state of a node of a search problem
// Copyright (c) 2018 Balint Gyevnar

using System;

namespace UninformedSearch.Search
{
    /// <summary>
    /// Class to represent arbitrary states for a search problem.
    /// </summary>
    public interface IState : ICloneable { }
}

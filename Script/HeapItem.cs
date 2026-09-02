using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;


    public interface IHeapItem<T> : IComparable<T>
    {
        int Id { get; set; }
      
    }

// <copyright file="Matrix.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using IK.Algorithms.Matrices.Constants;
using IK.Algorithms.Matrices.Exceptions;
using IK.Algorithms.Matrices.Interfaces;

namespace IK.Algorithms.Matrices.Implementations
{
    public class Matrix<TIdentifier, TValue> : IMatrix<TIdentifier, TValue>
    {
        private TIdentifier[] columnIdentifiers;
        private bool intialized;

        private TValue[][] matrix;
        private TIdentifier[] rowIdentifiers;

        public IEnumerable<TIdentifier> Rows
        {
            get
            {
                return this.rowIdentifiers;
            }
        }

        public IEnumerable<TIdentifier> Columns
        {
            get
            {
                return this.columnIdentifiers;
            }
        }

        public void Set(TIdentifier rowIdentifier, TIdentifier columnIdentifier, TValue element)
        {
            this.EnsureInitialized();
            int row = this.GetIndex(Dimensions.Row, rowIdentifier);
            int column = this.GetIndex(Dimensions.Column, columnIdentifier);
            this.matrix[row][column] = element;
        }

        public void Set(TIdentifier identifier, Dimensions dimension, params TValue[] elements)
        {
            this.EnsureInitialized();
            switch (dimension)
            {
                case Dimensions.Column:
                    int column = this.GetIndex(Dimensions.Column, identifier);
                    for (int i = 0; i < this.rowIdentifiers.Length; i++)
                    {
                        this.matrix[i][column] = elements[i];
                    }
                    return;
                case Dimensions.Row:
                    int row = this.GetIndex(Dimensions.Row, identifier);
                    this.matrix[row] = elements;
                    return;
                default:
                    return;
            }
        }

        public TValue Get(TIdentifier rowIdentifier, TIdentifier columnIdentifier)
        {
            this.EnsureInitialized();
            int row = this.GetIndex(Dimensions.Row, rowIdentifier);
            int column = this.GetIndex(Dimensions.Column, columnIdentifier);
            return this.matrix[row][column];
        }

        public void AddRows(params TIdentifier[] rows)
        {
            if (rows == null)
            {
                throw new ArgumentException("The collection of identifiers is empty.");
            }
            this.rowIdentifiers = rows;
            this.intialized = false;
            this.matrix = new TValue[rows.Length][];
        }

        public void AddColumns(params TIdentifier[] columns)
        {
            if (this.rowIdentifiers == null)
            {
                throw new ArgumentException("The collection of identifiers is empty.");
            }
            this.columnIdentifiers = columns;
            if (this.matrix != null)
            {
                for (int i = 0; i < this.rowIdentifiers.Length; i++)
                {
                    this.matrix[i] = new TValue[this.columnIdentifiers.Length];
                }
                this.intialized = true;
            }
            else
            {
                throw new NotInitializedException("Cannot add columns before adding rows. Add rows first.");
            }
        }

        private int GetIndex(Dimensions dimension, TIdentifier identifier)
        {
            switch (dimension)
            {
                case Dimensions.Column:
                    int column = Array.IndexOf(this.columnIdentifiers, identifier);
                    if (column < 0)
                    {
                        throw new IdentifierNotFoundException<TIdentifier>(Dimensions.Column, identifier, "");
                    }
                    return column;
                case Dimensions.Row:
                    int row = Array.IndexOf(this.rowIdentifiers, identifier);
                    if (row < 0)
                    {
                        throw new IdentifierNotFoundException<TIdentifier>(Dimensions.Row, identifier, "");
                    }
                    return row;
                default:
                    return -1;
            }
        }

        private void EnsureInitialized()
        {
            if (!this.intialized)
            {
                throw new NotInitializedException("The matrix has not been initialized. Please add rows and columns to the matrix.");
            }
        }
    }
}

﻿using Plukit.Base;

namespace NimbusFox.FoxCore.Classes {
    public class VectorCubeI {
        private Vector3I Start { get; }
        private Vector3I End { get; }

        public AreaI X { get; }
        public AreaI Y { get; }
        public AreaI Z { get; }

        public VectorCubeI(Vector3I start, Vector3I end) {
            Start = start;
            End = end;

            X = new AreaI(start.X, end.X);

            Y = new AreaI(start.Y, end.Y);

            Z = new AreaI(start.Z, end.Z);
        }

        public bool IsInside(Vector3I position) {
            return X.Start < position.X
                   && Y.Start < position.Y
                   && Z.Start < position.Z
                   && X.End > position.X
                   && Y.End > position.Y
                   && Z.End > position.Z;
        }

        public bool IsInside(Vector3D position) {
            return X.Start < position.X
                   && Y.Start < position.Y
                   && Z.Start < position.Z
                   && X.End > position.X
                   && Y.End > position.Y
                   && Z.End > position.Z;
        }
    }
}

namespace Kinetra.Physics
{
public enum Spin
    {
        Up = +1,
        Down = -1
    }

public class Electron
    {
        public int n; // energy level
        public char subshell;
        public Spin spin;
        public int orbitalIndex;

        public float ComputeZeff(Atom a)
        {
            float S = 0f;

            foreach (var e in a.Electrons)
            {
                if (e == this) continue;

                if (this.subshell == 's' || this.subshell == 'p')
                {
                    if (e.n == this.n)
                        S += (this.n == 1) ? 0.30f : 0.35f;

                    else if (e.n == this.n - 1)
                        S += 0.85f;

                    else if (e.n < this.n - 1)
                        S += 1.0f;
                }
                else // d or f
                {
                    if (e.n == this.n && e.subshell == this.subshell)
                        S += 0.35f;

                    else if (e.n < this.n)
                        S += 1.0f;
                }
            }

            return a.p - S;
        }
    }
}
using UnityEngine;

public class CVector2
{
    const float epsilon = 0.001f;

    private float x;
    private float y;

    public float X
    {
        get { return x; }
        set { x = value; }
    }
    public float Y
    {
        get { return y; }
        set { y = value; }
    }

    public CVector2()
    {
        x = 0f;
        y = 0f;
    }

    public CVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }

    public static CVector2 operator +(CVector2 v1, CVector2 v2)
    {
        return new CVector2(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static CVector2 operator -(CVector2 v1, CVector2 v2)
    {
        return new CVector2(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static CVector2 operator *(CVector2 v1, CVector2 v2)
    {
        return new CVector2(v1.X * v2.X, v1.Y * v2.Y);
    }

    public static CVector2 operator *(CVector2 v1, float s)
    {
        return new CVector2(v1.X * s, v1.Y * s);
    }

    public static bool operator ==(CVector2 v1, CVector2 v2)
    {
        if (Mathf.Abs(v1.X - v2.X) <= epsilon && Mathf.Abs(v1.Y - v2.Y) <= epsilon)
            return true;
        else
            return false;
    }

    public static bool operator !=(CVector2 v1, CVector2 v2)
    {
        return !(v1 == v2);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public static float Magnitude(CVector2 v1)
    {
        return Mathf.Sqrt(v1.X * v1.X + v1.Y * v1.Y);
    }

    public static float Distancia(CVector2 v1, CVector2 v2)
    {
        CVector2 diff = v2 - v1;
        return CVector2.Magnitude(diff);
    }

    public static CVector2 Normaliza(CVector2 v1)
    {
        float m = CVector2.Magnitude(v1);
        return new CVector2(v1.X / m, v1.Y / m);
    }
}

#include "vectors.h"
#include <math.h>

/// Vector unary operations

float vector_size(Vector v)
{
  return sqrt(vector_dot(v, v));
}

float vector_size_squared(Vector v)
{
  return vector_dot(v, v);
}

void vector_normalize(Vector *v)
{
  float size = vector_size(*v);

  vector_multiply_f(v, 1.f / size);
}

Vector vector_normalized(Vector v)
{
  float size = vector_size(v);

  return vector_multiplied_f(v, 1.f / size);
}

void vector_balance(Vector* v)
{
  float max = fmaxf(v -> X, fmaxf(v -> Y, v -> Z));

  if(max > 1)
  {
    v -> X /= max;
    v -> Y /= max;
    v -> Z /= max;
  }
}

void vector_clamp(Vector *v, float inf, float sup)
{
  v -> X = fmaxf(v -> X, inf);
  v -> X = fminf(v -> X, sup);

  v -> Y = fmaxf(v -> Y, inf);
  v -> Y = fminf(v -> Y, sup);

  v -> Z = fmaxf(v -> Z, inf);
  v -> Z = fminf(v -> Z, sup);
}

Vector vector_clamped(Vector v, float inf, float sup)
{
  Vector clamP;

  clamP.X = fmaxf(v.X, inf);
  clamP.X = fminf(clamP.X, sup);

  clamP.Y = fmaxf(v.Y, inf);
  clamP.Y = fminf(clamP.Y, sup);

  clamP.Z = fmaxf(v.Z, inf);
  clamP.Z = fminf(clamP.Z, sup);

  return clamP;
}

/// Vector - Float factor operations

void vector_multiply_f(Vector *v, float c)
{
  v -> X *= c;
  v -> Y *= c;
  v -> Z *= c;
}

Vector vector_multiplied_f(Vector v, float c)
{
  Vector multiP;

  multiP.X = v.X * c;
  multiP.Y = v.Y * c;
  multiP.Z = v.Z * c;

  return multiP;
}

void vector_divide_f(Vector *v, float c)
{
  vector_multiply_f(v, 1.f / c);
}

Vector vector_divided_f(Vector v, float c)
{
  return vector_multiplied_f(v, 1.f / c);
}

/// Vector - Vector factor operations

/** Multiplica cada componente del vector con su componente
    respectiva en otro vector. */
void vector_multiply_v(Vector *v, Vector a)
{
  v -> X *= a.X;
  v -> Y *= a.Y;
  v -> Z *= a.Z;
}

/** Entrega en un vector, el resultado de multiplicar
    las mismas componentes de ambos vectores. */
Vector vector_multiplied_v(Vector v1, Vector v2)
{
  Vector multiP;

  multiP.X = v1.X * v2.X;
  multiP.Y = v1.Y * v2.Y;
  multiP.Z = v1.Z * v2.Z;

  return multiP;
}

/** Divide cada componente del vector con su componente
    respectiva en otro vector. */
void vector_divide_v(Vector *v, Vector a)
{
  v -> X /= a.X;
  v -> Y /= a.Y;
  v -> Z /= a.Z;
}

/** Entrega en un vector, el resultado de dividir
    las componentes de v1 por las de v2. */
Vector vector_divided_v(Vector v1, Vector v2)
{
  Vector diviD;

  diviD.X = v1.X / v2.X;
  diviD.Y = v1.Y / v2.Y;
  diviD.Z = v1.Z / v2.Z;

  return diviD;
}

/// Vector - Float addition & substraction

void vector_add_f(Vector *v, float c)
{
  v -> X += c;
  v -> Y += c;
  v -> Z += c;
}

Vector vector_added_f(Vector v, float c)
{
  Vector addeD;

  addeD.X = v.X + c;
  addeD.Y = v.Y + c;
  addeD.Z = v.Z + c;

  return addeD;
}

void vector_substract_f(Vector *v, float c)
{
    v -> X -= c;
    v -> Y -= c;
    v -> Z -= c;
}

Vector vector_substracted_f(Vector v, float c)
{
  Vector subS;

  subS.X = v.X - c;
  subS.Y = v.Y - c;
  subS.Z = v.Z - c;

  return subS;
}

/// Vector - Vector addition & substraction

void vector_add_v(Vector *v, Vector a)
{
  v -> X += a.X;
  v -> Y += a.Y;
  v -> Z += a.Z;
}

Vector vector_added_v(Vector v, Vector a)
{
  Vector addeD;

  addeD.X = v.X + a.X;
  addeD.Y = v.Y + a.Y;
  addeD.Z = v.Z + a.Z;

  return addeD;
}

void vector_substract_v(Vector *v, Vector a)
{
  v -> X -= a.X;
  v -> Y -= a.Y;
  v -> Z -= a.Z;
}

Vector vector_substracted_v(Vector v, Vector a)
{
  /* Versión antigua, usar si la actual falla
  Vector subS;

  subS.X = v.X - a.X;
  subS.Y = v.Y - a.Y;
  subS.Z = v.Z - a.Z;

  return subS;
  */

  return (Vector){.X = v.X - a.X, .Y = v.Y - a.Y, .Z = v.Z - a.Z};
}

/// Vector geometric operations

float vector_dot(Vector v1, Vector v2)
{
  return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
}

Vector vector_cross(Vector v1, Vector v2)
{
  Vector theCross;

  theCross.X = v1.Y * v2.Z - v1.Z * v2.Y;
  theCross.Y = v1.Z * v2.X - v1.X * v2.Z;
  theCross.Z = v1.X * v2.Y - v1.Y * v2.X;

  return theCross;
}

/// Misc multi vector operations

Vector vector_blend2(Vector v1, Vector v2, float u)
{
  Vector blenD;

  blenD.X = v1.X * u + v2.X * (1.f - u);
  blenD.Y = v1.Y * u + v2.Y * (1.f - u);
  blenD.Z = v1.Z * u + v2.Z * (1.f - u);

  return blenD;
}

Vector vector_blend3(Vector v1, Vector v2, Vector v3, float u, float v)
{
  Vector blenD;

  blenD.X = v1.X * u + v2.X * v + v3.X * (1.f - u - v);
  blenD.Y = v1.Y * u + v2.Y * v + v3.Y * (1.f - u - v);
  blenD.Z = v1.Z * u + v2.Z * v + v3.Z * (1.f - u - v);

  return blenD;
}

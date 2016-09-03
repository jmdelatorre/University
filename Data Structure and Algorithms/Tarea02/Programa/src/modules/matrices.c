#include "matrices.h"
#include <math.h>

float matrix_determinant(Matrix m)
{
  return
    m.XX * (m.YY * m.ZZ - m.ZY * m.YZ)
    - m.YX * (m.XY * m.ZZ - m.ZY * m.XZ)
    + m.ZX * (m.XY * m.YZ - m.YY * m.XZ);
}

Matrix matrix_inverse(Matrix m)
{
  return m;
}

Matrix matrix_transposed(Matrix m)
{
  return (Matrix)
  {
    .XX = m.XX, .XY = m.YX, .XZ = m.ZX,
    .YX = m.XY, .YY = m.YY, .YZ = m.ZY,
    .ZX = m.XZ, .ZY = m.YZ, .ZZ = m.ZZ
  };
}

Matrix matrix_getRotation(float thetaX, float thetaY, float thetaZ)
{
  Matrix rotX = Matrix_Identity;
  rotX.YY = cos(thetaX);
  rotX.YZ = sin(thetaX);
  rotX.ZY = -sin(thetaX);
  rotX.ZZ = cos(thetaX);

  Matrix rotY = Matrix_Identity;
  rotY.ZZ = cos(thetaY);
  rotY.ZX = sin(thetaY);
  rotY.XZ = -sin(thetaY);
  rotY.XX = cos(thetaY);

  Matrix rotZ = Matrix_Identity;
  rotZ.XX = cos(thetaZ);
  rotZ.XY = sin(thetaZ);
  rotZ.YX = -sin(thetaZ);
  rotZ.YY = cos(thetaZ);

  return matrix_multipliedMM(rotZ, matrix_multipliedMM(rotY, rotX));
}

Matrix matrix_getScaling(float scaleX, float scaleY, float scaleZ)
{
  Matrix scale = Matrix_Identity;

  scale.XX = scaleX;
  scale.YY = scaleY;
  scale.ZZ = scaleZ;

  return scale;
}

Matrix matrix_multipliedMM(Matrix mLeft, Matrix mRight)
{
  Matrix mult;

  mult.XX =
  mLeft.XX * mRight.XX + mLeft.XY * mRight.YX + mLeft.XZ * mRight.ZX;
  mult.XY =
  mLeft.XX * mRight.XY + mLeft.XY * mRight.YY + mLeft.XZ * mRight.ZY;
  mult.XZ =
  mLeft.XX * mRight.XZ + mLeft.XY * mRight.YZ + mLeft.XZ * mRight.ZZ;

  mult.YX =
  mLeft.YX * mRight.XX + mLeft.YY * mRight.YX + mLeft.YZ * mRight.ZX;
  mult.YY =
  mLeft.YX * mRight.XY + mLeft.YY * mRight.YY + mLeft.YZ * mRight.ZY;
  mult.YZ =
  mLeft.YX * mRight.XZ + mLeft.YY * mRight.YZ + mLeft.YZ * mRight.ZZ;

  mult.ZX =
  mLeft.ZX * mRight.XX + mLeft.ZY * mRight.YX + mLeft.ZZ * mRight.ZX;
  mult.ZY =
  mLeft.ZX * mRight.XY + mLeft.ZY * mRight.YY + mLeft.ZZ * mRight.ZY;
  mult.ZZ =
  mLeft.ZX * mRight.XZ + mLeft.ZY * mRight.YZ + mLeft.ZZ * mRight.ZZ;

  return mult;
}

void matrix_multiplyMV(Matrix m, Vector *v)
{
  m.M00 *= v -> X;
  m.M01 *= v -> Y;
  m.M02 *= v -> Z;

  m.M10 *= v -> X;
  m.M11 *= v -> Y;
  m.M12 *= v -> Z;

  m.M20 *= v -> X;
  m.M21 *= v -> Y;
  m.M22 *= v -> Z;

  v -> X = m.M00 + m.M01 + m.M02;
  v -> Y = m.M10 + m.M11 + m.M12;
  v -> Z = m.M20 + m.M21 + m.M22;
}

Vector matrix_multipliedMV(Matrix m, Vector v)
{
  Vector mulT;
  mulT.X = m.M00 * v.X + m.M01 * v.Y + m.M02 * v.Z;
  mulT.Y = m.M10 * v.X + m.M11 * v.Y + m.M12 * v.Z;
  mulT.Z = m.M20 * v.X + m.M21 * v.Y + m.M22 * v.Z;

  return mulT;
}

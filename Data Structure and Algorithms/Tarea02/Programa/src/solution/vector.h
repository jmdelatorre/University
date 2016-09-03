#ifndef T2_LIB_VECTOR_W
#define T2_LIB_VECTOR_W

struct vector
{
  /** Componente X del vector */
  float X;
  /** Componente Y del vector */
  float Y;
  /** Componente Z del vector*/
  float Z;
};

/** Define un vector en R³ */
typedef struct vector Vector;

/** Obtiene la magnitud de un vector*/
float  vector_size            (Vector  v);

/** Normaliza un vector */
void   vector_normalize       (Vector* v);

/** Entrega una versión normalizada de v, sin modificar a v.*/
Vector vector_normalized      (Vector  v);

/** Multiplica al vector por un escalar. */
void   vector_multiply_f      (Vector* v, float c);

/** Entrega el resultado de multiplicar a v por c. */
Vector vector_multiplied_f    (Vector  v, float c);

/** Divide al vector por un escalar. */
void   vector_divide_f        (Vector* v, float c);

/** Entrega el resultado de dividir a v en c. */
Vector vector_divided_f       (Vector  v, float c);

/** Multiplica los vectores componente a componente */
void   vector_multiply_v      (Vector* v, Vector w);

/** Entrega el resultado de multiplicar los vectores componente a componente */
Vector vector_multiplied_v    (Vector  v, Vector w);

/** Le suma al vector otro vector */
void   vector_add_v           (Vector* v, Vector w);

/** Entrega el resultado de sumar ambos vectores */
Vector vector_added_v         (Vector  v, Vector w);

/** Le resta a al vector v */
void   vector_substract_v     (Vector* v, Vector w);

/** Entrega el resultado de restar ambos vectores */
Vector vector_substracted_v   (Vector  v, Vector w);

/** Entrega el resultado del producto punto entre ambos vectores */
float  vector_dot             (Vector  v, Vector w);

/** Entrega el resultado del producto cruz entre ambos vectores.*/
Vector vector_cross           (Vector  v, Vector w);

#endif /* end of include guard: T2_LIB_VECTOR_W */

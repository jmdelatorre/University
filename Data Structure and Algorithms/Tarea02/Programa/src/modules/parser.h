#ifndef T2_LIB_WAVEFRONT
#define T2_LIB_WAVEFRONT

/*#########################################################################*/
/* Parser de archivos Wavefront .obj y .mtl                                */
/* Código construido a partir de http://www.kixor.net/dev/objloader/       */
/*#########################################################################*/

typedef struct
{
	int item_count;
	int current_max_size;
	char growable;

	void **items;
	char **names;
} list;

int list_find(list *listo, char *name_to_find);

/*#########################################################################*/
/*############################   Material   ###############################*/
/*#########################################################################*/

#define MATERIAL_NAME_SIZE 255
#define MTL_FILENAME_LENGTH 512
#define MTL_LINE_SIZE 256

typedef struct
{
	char name[MATERIAL_NAME_SIZE];
	char diffuse_texture_filename[MTL_FILENAME_LENGTH];
	char normal_texture_filename[MTL_FILENAME_LENGTH];
	double amb[3];
	double diff[3];
	double spec[3];
	double reflect;
	double refract;
	double trans;
	double shiny;
	double glossy;
	double refract_index;
} mtl_material;

typedef struct
{
	list material_list;
} material_data;

/*#########################################################################*/
/*#############################   Object   ################################*/
/*#########################################################################*/

#define OBJ_FILENAME_LENGTH 500
#define OBJ_LINE_SIZE 500
#define MAX_VERTEX_COUNT 4 //can only handle quads or triangles

typedef struct
{
	int vertex_index[MAX_VERTEX_COUNT];
	int normal_index[MAX_VERTEX_COUNT];
	int texture_index[MAX_VERTEX_COUNT];
	int vertex_count;
	int material_index;
}obj_face;

typedef struct
{
	int pos_index;
	int up_normal_index;
	int equator_normal_index;
	int texture_index[MAX_VERTEX_COUNT];
	int material_index;
}obj_sphere;

typedef struct
{
	int pos_index;
	int normal_index;
	int rotation_normal_index;
	int texture_index[MAX_VERTEX_COUNT];
	int material_index;
}obj_plane;

typedef struct
{
	double e[3];
} obj_vector;

typedef struct
{
	int camera_pos_index;
	int camera_look_point_index;
	int camera_up_norm_index;
} obj_camera;

typedef struct
{
	int pos_index;
	int material_index;
} obj_light_point;

typedef struct
{
	int pos_index;
	int normal_index;
	int material_index;
} obj_light_disc;

typedef struct
{
	int vertex_index[MAX_VERTEX_COUNT];
	int material_index;
}obj_light_quad;

typedef struct
{
	char scene_filename[OBJ_FILENAME_LENGTH];
	char material_filename[OBJ_FILENAME_LENGTH];

	list vertex_list;
	list vertex_normal_list;
	list vertex_texture_list;

	list face_list;
	list sphere_list;
	list plane_list;

	list light_point_list;
	list light_quad_list;
	list light_disc_list;

	obj_camera *camera;
} obj_growable_scene_data;

typedef struct
{
	obj_vector **vertex_list;
	obj_vector **vertex_normal_list;
	obj_vector **vertex_texture_list;

	obj_face **face_list;
	obj_sphere **sphere_list;
	obj_plane **plane_list;

	obj_light_point **light_point_list;
	obj_light_quad **light_quad_list;
	obj_light_disc **light_disc_list;

	int vertex_count;
	int vertex_normal_count;
	int vertex_texture_count;

	int face_count;
	int sphere_count;
	int plane_count;

	int light_point_count;
	int light_quad_count;
	int light_disc_count;

	obj_camera *camera;
} obj_scene_data;

/*#########################################################################*/
/*#############################   Loader   ################################*/
/*#########################################################################*/

typedef struct
{
	obj_vector **vertexList;
	obj_vector **normalList;
	obj_vector **textureList;

	obj_face **faceList;
	obj_sphere **sphereList;
	obj_plane **planeList;

	obj_light_point **lightPointList;
	obj_light_quad **lightQuadList;
	obj_light_disc **lightDiscList;

	int vertexCount;
	int normalCount;
	int textureCount;

	int faceCount;
	int sphereCount;
	int planeCount;

	int lightPointCount;
	int lightQuadCount;
	int lightDiscCount;

	obj_camera *camera;
	obj_scene_data data;
} objLoader;


typedef struct
{
	list material_list;

  material_data data;
} mtlLoader;

int  mtl_loader_load(mtlLoader* loader, char *filename);
void mtl_loader_destroy(mtlLoader* loader);

int  obj_loader_load(objLoader* loader, char *filename, int current_material, material_data* materials);
void obj_loader_destroy(objLoader* loader);



#endif /* end of include guard: T2_LIB_WAVEFRONT */

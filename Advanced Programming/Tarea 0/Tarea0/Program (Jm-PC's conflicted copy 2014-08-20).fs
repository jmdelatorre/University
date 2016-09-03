// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open LibreriaT0
open System
let interfaz = new Interfaz(true)
let caca= 6

let humanos = interfaz.GetArregloHumanos()
let consulta = interfaz.EsperarConsulta()


if consulta = 1 then 
    let restriccionConsulta0 = interfaz.GetRestriccionesConsulta1()
    let tipo= restriccionConsulta0.tipo_publicacion
    let mayoresDe= restriccionConsulta0.contiene_mayores_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser mayores o iguales a la edad entregada
    let menoresDe= restriccionConsulta0.contiene_menores_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser menores o iguales a la edad entregada
    let realizadaDespues= restriccionConsulta0.realizada_despues_de //Las publicaciones deben haber sido realizadas DESPUES de la fecha entregada.
    let realizadaAntes= restriccionConsulta0.realizada_despues_de //Las publicaciones deben haber sido realizadas Antes de la fecha entregada.
    let contieneA= restriccionConsulta0.contiene_a //Las publicaciones deben etiquetar a ALGUNA de las personas includas en el arreglo. El arreglo posee los NOMBRES de las personas involucradas.
    let NoContieneA= restriccionConsulta0.no_contiene_a // Las publicaciones no deben etiquetar a NINGUNA de las personas includas en el arreglo. El arreglo posee los NOMBRES de las personas involucradas.


    let humanosID = [for i in humanos -> i]


    let HumanosFiltradosEdad (mayoresDe:int) (menoresDe:int) = // Filtrar por edad
       match mayoresDe with
       | -1 -> match menoresDe with
              | -1 -> humanosID
              |_ ->  List.filter (fun  x -> int x.[2] <= menoresDe) humanosID
       |_ -> match menoresDe with
              | -1 ->  List.filter (fun  x -> int x.[2] >= mayoresDe) humanosID
              |_ ->  List.filter (fun  x -> int x.[2] >= mayoresDe && int x.[2] <= menoresDe) humanosID


    let HumanosFiltradosPorEdad = HumanosFiltradosEdad mayoresDe menoresDe

    let rec DatoHumano x y = // Manipulacion humanos retorna un dato en especifico de todos los humanos
        if x < HumanosFiltradosPorEdad.Length then  
          let muro =  [HumanosFiltradosPorEdad.[x].[y]]
          let rest = DatoHumano (x+1) y
          List.append muro rest     
        else  []


    let wallID = [for i in humanos -> i.[4]  ]  // lista con el wall ID de todos los humanos
    let wallIDfiltrado = DatoHumano 0 4
    let muro =  [ for informacion in interfaz.GetMuroHumano(wallID.Item(0)) -> informacion.[1] ] // lista de los compadres que postearon al loco 0
    let publicacion = muro.[0].[1] // ID del que publico el post
 

    let Publicaciones = // Lista con todas las publicaciones
      wallIDfiltrado
      |> List.map (fun publicacion -> interfaz.GetMuroHumano(publicacion) |> Array.toList)
      |> List.concat


    let rec EntregoEtiquetaBoolean (etiqueta:string []) (mayoresDe:int) (menoresDe:int) x y = // Filtrar por edad
        if x < etiqueta.Length then
         if etiqueta.[x]="-" then
           if y < humanos.[y].Length then
            if etiqueta.[x] = humanos.[y].[0] then
              if  int humanos.[y].[2] >= mayoresDe then
                if int humanos.[y].[2] <= menoresDe then
                 true
                else
                 EntregoEtiquetaBoolean etiqueta mayoresDe menoresDe (x+1) 0 
              else 
                 EntregoEtiquetaBoolean etiqueta mayoresDe menoresDe (x+1) 0
            else 
                EntregoEtiquetaBoolean etiqueta mayoresDe menoresDe x (y+1)
           else
             EntregoEtiquetaBoolean etiqueta mayoresDe menoresDe (x+1) 0
         else true
        else false
    
 
    let PublicacionesFiltradasEtiqueta =
         Publicaciones
        |> List.filter (fun x -> EntregoEtiquetaBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) mayoresDe menoresDe 0 0)
    


    let PublicacionesFiltradasTipo tipo = //filtrar por tipo
        match tipo with
        |"f" -> PublicacionesFiltradasEtiqueta |> List.filter   (fun  x -> x.[3] = tipo  )  // filta por tipo
        |"p" -> PublicacionesFiltradasEtiqueta |> List.filter   (fun  x -> x.[3] = tipo  )  // filta por tipo
        |_ -> PublicacionesFiltradasEtiqueta



    let PublicacionesFiltradas = PublicacionesFiltradasTipo tipo
    printfn "%A" PublicacionesFiltradas


    let rec DatoPublicacion x y (z:string [] list) = // Manipulacion Listas retorna un dato en especifico para todos las publicaciones
     if x < z.Length then  
          let muro =  [z.[x].[y]]
          let rest = DatoPublicacion (x+1) y z
          List.append muro rest     
     else  []

    let etiquetas= DatoPublicacion 0 6 PublicacionesFiltradas

    let ListaTags = // Lista todos los etiquetados
      etiquetas
      |> List.map (fun publicacion -> interfaz.GetEtiquetasPublicacion(publicacion) |> Array.toList )
      |> List.concat


    let tiempoantes = System. realizadaAntes
    let tiempodespues = System.DateTime.Parse realizadaDespues
    let comprartiempo = System.DateTime.Compare (timepoantes tiempodespues)


    let PublicacionesFiltradasEdades mayoresDe menoresDe =
        ListaTags


    let autor = Publicaciones.[0].[1]
    let etiqueta = interfaz.GetEtiquetasPublicacion(Publicaciones.[0].[6])
    printfn "%s" autor
    printfn "%A" etiqueta



    let ListaAutoresPublicaciones= DatoPublicacion 0 1 PublicacionesFiltradas


    let grouped = //Agrupa todas las publicaciones echas por un mismo Humano
        ListaAutoresPublicaciones 
        |> Seq.countBy  (id)
        |> Seq.toList
        |> List.sortBy(fun (_, s) -> s)
        |> List.rev

    printfn "%A" grouped
    printfn "%A" humanosID

    printfn "%A" grouped


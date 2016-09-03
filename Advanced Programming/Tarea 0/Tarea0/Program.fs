// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open LibreriaT0
open System
open System.Globalization
let interfaz = new Interfaz()
while true do
let consulta = interfaz.EsperarConsulta()

let humanos = interfaz.GetArregloHumanos()

let humanosID = [for i in humanos -> i]
let humanosIDseguidos= humanosID |> List.map (fun x -> interfaz.GetHumanosSeguidos(x.[5])|>Array.toList)  |> List.concat //ID de los humanos seguidos (algunos no estan en el arreglo humanos)
let todosLosHumanosRepetidos = List.append humanosID humanosIDseguidos 

let TodosLosHumanos = // Lista con Todos los humanos. Incluido los que no estan en el arreglo
    todosLosHumanosRepetidos
    |> Seq.distinctBy (fun a-> a.[1])
    |> Seq.toList

let HumanosFiltradosEdad (mayoresDe:int) (menoresDe:int) = // Filtrar por edad
    match mayoresDe with
    | -1 -> match menoresDe with
            | -1 -> TodosLosHumanos
            |_ ->  List.filter (fun  x -> int x.[2] <= menoresDe) TodosLosHumanos
    |_ -> match menoresDe with
            | -1 ->  List.filter (fun  x -> int x.[2] >= mayoresDe) TodosLosHumanos
            |_ ->  List.filter (fun  x -> int x.[2] >= mayoresDe && int x.[2] <= menoresDe) TodosLosHumanos


let rec DatoHumano x y (z:string[] list) = // Manipulacion humanos retorna un dato en especifico de todos los humanos (z= lista por filtrar)
    if x < z.Length then  
        let muro =  [z.[x].[y]]
        let rest = DatoHumano (x+1) y z
        List.append muro rest     
    else  []

let rec EntregoEtiquetaMayorBoolean (etiqueta:string []) mayoresDe x y = // Filtrar por edad etiqueta mayor
    if x < etiqueta.Length then
        if y < TodosLosHumanos.[y].Length then
            if etiqueta.[x] = TodosLosHumanos.[y].[0] then
                if  int TodosLosHumanos.[y].[2] >=  mayoresDe then
                EntregoEtiquetaMayorBoolean etiqueta mayoresDe  (x+1) 0                 
                else
                    false
            else 
                EntregoEtiquetaMayorBoolean etiqueta mayoresDe  x (y+1)
        else
            EntregoEtiquetaMayorBoolean etiqueta mayoresDe  (x+1) 0
    else true


let rec EntregoEtiquetaMenorBoolean (etiqueta:string []) menoresDe x y = // Filtrar por edad etiqueta menor
    if x < etiqueta.Length then
        if y < TodosLosHumanos.[y].Length then
            if etiqueta.[x] = TodosLosHumanos.[y].[0] then
                if  int TodosLosHumanos.[y].[2] <= int menoresDe then
                    EntregoEtiquetaMenorBoolean etiqueta menoresDe  (x+1) 0                 
                else
                    false
            else 
                EntregoEtiquetaMenorBoolean etiqueta menoresDe  x (y+1)
        else
            EntregoEtiquetaMenorBoolean etiqueta menoresDe  (x+1) 0
    else true


let rec EntregoContieneBoolean (etiquetado:string []) (contieneA:string []) x y z = // Filtrar por contitene
    if z < contieneA.Length then
        if x < etiquetado.Length then
            if etiquetado.[x] <> "-" then
                if  y < TodosLosHumanos.[y].Length  then
                    if etiquetado.[x] = TodosLosHumanos.[y].[0]  then
                        if  TodosLosHumanos.[y].[1] = contieneA.[z]  then
                            true
                        else
                            EntregoContieneBoolean etiquetado contieneA  0 0 (z+1)
                    else 
                        EntregoContieneBoolean etiquetado contieneA  x (y+1) z 
                else 
                    EntregoContieneBoolean etiquetado contieneA  (x+1) (0) z 
            else 
                false
        else
            false 
    else
        false 
         
let rec EntregoNoContieneBoolean (etiquetado:string []) (NoContieneA:string []) x y z =  // Filtrar por Nocontitene
    if z < NoContieneA.Length then
        if x < etiquetado.Length then
            if etiquetado.[x] <> "-" then
                if  y < TodosLosHumanos.[y].Length  then
                    if etiquetado.[x] = TodosLosHumanos.[y].[0]  then
                        if  TodosLosHumanos.[y].[1] = NoContieneA.[z]  then
                            false
                        else
                            EntregoNoContieneBoolean etiquetado NoContieneA  0 0 (z+1)
                    else 
                        EntregoNoContieneBoolean etiquetado NoContieneA  x (y+1) z 
                else 
                    EntregoNoContieneBoolean etiquetado NoContieneA  (x+1) (0) z 
            else 
                true
        else
            true 
    else
        true 
let rec DatoPublicacion x y (z:string [] list) = // Manipulacion Listas retorna un dato en especifico para todos las publicaciones
    if x < z.Length then  
        let muro =  [z.[x].[y]]
        let rest = DatoPublicacion (x+1) y z
        List.append muro rest     
    else  []

let publication idx (lst: string [] list) = // Manipulacion Listas retorna un dato en especifico para todos las publicaciones (otra forma más eficiente)
    lst
    |> List.map (fun arr -> arr.[idx])

let rec  datoHumano (humanosFinal:string [] list) x y z = // entrego un AuthorID (y) y me retorna algun dato especifico(z) del humano 
    if x < humanosFinal.Length then
        if humanosFinal.[x].[0] = y then
            humanosFinal.[x].[z]
        else datoHumano humanosFinal (x+1) y z
    else
        "hola"

if consulta = 1 then 
    let restriccionConsulta0 = interfaz.GetRestriccionesConsulta1()
    let tipo= restriccionConsulta0.tipo_publicacion
    let mayoresDe= restriccionConsulta0.contiene_mayores_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser mayores o iguales a la edad entregada
    let menoresDe= restriccionConsulta0.contiene_menores_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser menores o iguales a la edad entregada
    let realizadaDespues= restriccionConsulta0.realizada_despues_de //Las publicaciones deben haber sido realizadas DESPUES de la fecha entregada.
    let realizadaAntes= restriccionConsulta0.realizada_antes_de //Las publicaciones deben haber sido realizadas Antes de la fecha entregada.
    let contieneA= restriccionConsulta0.contiene_a //Las publicaciones deben etiquetar a ALGUNA de las personas includas en el arreglo. El arreglo posee los NOMBRES de las personas involucradas.
    let NoContieneA= restriccionConsulta0.no_contiene_a // Las publicaciones no deben etiquetar a NINGUNA de las personas includas en el arreglo. El arreglo posee los NOMBRES de las personas involucradas.

    let HumanosFiltradosPorEdad = HumanosFiltradosEdad mayoresDe menoresDe // lista con los humanos filtrados por su edad

    let wallIDfiltrado = DatoHumano 0 4 HumanosFiltradosPorEdad // lista con el Wall ID de todos los humanos filtrados por edad

    let Publicaciones = // Lista con todas las publicaciones filtrados por los humanos que cumplen filtros
      wallIDfiltrado
      |> List.map (fun publicacion -> interfaz.GetMuroHumano(publicacion) |> Array.toList)
      |> List.concat
       
    let PublicacionesFiltradasEtiqueta1 (Publicaciones:string [] list) mayoresDe menoresDe = // Filtrar por edad humanos etiquetados
       match mayoresDe with
       | -1 -> match menoresDe with
              | -1 -> Publicaciones
              |_ -> Publicaciones |> List.filter (fun x -> EntregoEtiquetaMenorBoolean (interfaz.GetEtiquetasPublicacion( x.[6])) (int menoresDe) 0 0) 
       |_ -> match menoresDe with
              | -1 ->  Publicaciones |> List.filter (fun  x ->  EntregoEtiquetaMayorBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) (int mayoresDe) 0 0   )
              |_ ->  Publicaciones |> List.filter (fun  x -> EntregoEtiquetaMenorBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) (int menoresDe) 0 0 && EntregoEtiquetaMayorBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) (int mayoresDe) 0 0     )

    let PublicacionesFiltradasEtiqueta = PublicacionesFiltradasEtiqueta1 Publicaciones mayoresDe menoresDe // Publicaciones filtradas por Etiqueta

    let PublicacionesFiltradasTipo
     tipo = //filtrar por tipo
        match tipo with
        |"f" -> PublicacionesFiltradasEtiqueta |> List.filter   (fun  x -> x.[3] = tipo  )  // filta por tipo
        |"p" -> PublicacionesFiltradasEtiqueta |> List.filter   (fun  x -> x.[3] = tipo  )  // filta por tipo
        |_ -> PublicacionesFiltradasEtiqueta

    let PublicacionesFiltradasTipoOK = PublicacionesFiltradasTipo tipo

    let fechaBooleanAntes antesDe fecha = // Boolean Antes de lo utiliza filtrar por fecha
        if System.DateTime.Compare (System.DateTime.ParseExact (antesDe, "d/M/yyyy", CultureInfo.InvariantCulture ), System.DateTime.ParseExact (fecha, "d/M/yyyy", CultureInfo.InvariantCulture )) = 1 then
         true
        else false

    let fechaBooleanDespues despuesDe fecha = // Boolean Antes de lo utiliza filtrar por fecha
        if System.DateTime.Compare (System.DateTime.ParseExact (despuesDe, "d/M/yyyy", CultureInfo.InvariantCulture ), System.DateTime.ParseExact (fecha, "d/M/yyyy", CultureInfo.InvariantCulture )) = -1 then
         true
        else false

    
    let PublicacioneFiltrarFecha (PublicacionesFiltradasTipoOK:string [] list) antesDe despuesDe = // Filtrar por fecha
       match antesDe with
       | "-" -> match despuesDe with
              | "-" -> PublicacionesFiltradasTipoOK
              |_ -> PublicacionesFiltradasTipoOK |> List.filter (fun  x ->  fechaBooleanDespues despuesDe x.[4]   )
       |_ -> match despuesDe with
              | "-" ->  PublicacionesFiltradasTipoOK |> List.filter (fun  x ->  fechaBooleanAntes antesDe x.[4]   )
              |_ ->  PublicacionesFiltradasTipoOK |> List.filter (fun  x -> fechaBooleanAntes antesDe x.[4]  && fechaBooleanDespues despuesDe x.[4]   )

    let PublicacionesFiltradasFechaTipoEtiqueta = PublicacioneFiltrarFecha PublicacionesFiltradasTipoOK realizadaAntes realizadaDespues //juntar filtros

    let PublicacionesFinal1 (PublicacionesFiltradasFechaTipoEtiqueta:string [] list) (NoContieneA:string[]) (contieneA:string[]) = // Filtrar Contieene o no contiene
        match NoContieneA with
        |[||] -> match contieneA with
                |[||] -> PublicacionesFiltradasFechaTipoEtiqueta
                |_ ->  PublicacionesFiltradasFechaTipoEtiqueta |> List.filter (fun x -> EntregoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) contieneA  0 0 0) 
        |_ -> match contieneA with
                |[||] ->PublicacionesFiltradasFechaTipoEtiqueta |> List.filter (fun x -> EntregoNoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) NoContieneA  0 0 0) 
                |_ -> PublicacionesFiltradasFechaTipoEtiqueta |> List.filter (fun x -> EntregoNoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) NoContieneA  0 0 0 && EntregoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) contieneA  0 0 0) 


    
    let PublicacionesFinal = PublicacionesFinal1 PublicacionesFiltradasFechaTipoEtiqueta NoContieneA contieneA // Publicaciones Filtradas Final
    
    let ListaAutoresPublicaciones= publication  1 PublicacionesFinal // utilizando una funcion del principio se agrupan solo los autores de las publicaciones

    let grouped = //Agrupa todas las publicaciones echas por un mismo Humano
        ListaAutoresPublicaciones 
        |> Seq.groupBy  (id)
        |> Seq.toList
        |> List.sortBy(fun (_, s) -> Seq.length s)
        |> List.rev

    let humanID =   List.map fst grouped // solo el authorID de los humanos

    let tupla1 (ids:string[]) (info:string[][]) = 
        ids 
        |> Seq.map (fun id -> id , info |> Seq.filter (fun y -> y.[1] = id) |> Seq.map (fun record -> record.[0]) |> Seq.toArray) 
        |> Seq.toArray


    let final =  tupla1  (humanID |>List.toArray) (PublicacionesFinal |>List.toArray) //ntrega la tupla final
    
    let RespuestaConsulta1 = interfaz.EntregarSolucionConsulta1(final)

    printfn "Consulta 1"

if consulta = 2 then 
    let restriccionConsulta1 = interfaz.GetRestriccionesConsulta2()
    let tipo= restriccionConsulta1.tipo_publicacion
    let mayoresDe= restriccionConsulta1.contiene_mayores_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser mayores o iguales a la edad entregada
    let menoresDe= restriccionConsulta1.contiene_menores_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser menores o iguales a la edad entregada
    let realizadaDespues= restriccionConsulta1.realizada_despues_de //Las publicaciones deben haber sido realizadas DESPUES de la fecha entregada.
    let realizadaAntes= restriccionConsulta1.realizada_antes_de //Las publicaciones deben haber sido realizadas Antes de la fecha entregada.
    let contieneA= restriccionConsulta1.contiene_a //Las publicaciones deben etiquetar a ALGUNA de las personas includas en el arreglo. El arreglo posee los NOMBRES de las personas involucradas.
    let NoContieneA= restriccionConsulta1.no_contiene_a // Las publicaciones no deben etiquetar a NINGUNA de las personas includas en el arreglo. El arreglo posee los NOMBRES de las personas involucradas.
    
    let HumanosFiltradosPorEdad = HumanosFiltradosEdad mayoresDe menoresDe // Humanos Filtrados por edad
    let wallIDfiltrado = DatoHumano 0 4 HumanosFiltradosPorEdad // lista con el Wall ID de todos los humanos filtrados por edad

    let Publicaciones = // Lista con todas las publicaciones donde los publicados cumplen las restricciones
      wallIDfiltrado
      |> List.map (fun publicacion -> interfaz.GetMuroHumano(publicacion) |> Array.toList)
      |> List.concat

        
    let PublicacionesFiltradasEtiqueta1 (Publicaciones:string [] list) mayoresDe menoresDe = // Filtrar por edad etiqueta
       match mayoresDe with
       | -1 -> match menoresDe with
              | -1 -> Publicaciones
              |_ -> Publicaciones |> List.filter (fun x -> EntregoEtiquetaMenorBoolean (interfaz.GetEtiquetasPublicacion( x.[6])) (int menoresDe) 0 0) 
       |_ -> match menoresDe with
              | -1 ->  Publicaciones |> List.filter (fun  x ->  EntregoEtiquetaMayorBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) (int mayoresDe) 0 0   )
              |_ ->  Publicaciones |> List.filter (fun  x -> EntregoEtiquetaMenorBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) (int menoresDe) 0 0 && EntregoEtiquetaMayorBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) (int mayoresDe) 0 0     )

    let PublicacionesFiltradasEtiqueta = PublicacionesFiltradasEtiqueta1 Publicaciones mayoresDe menoresDe // Publicaciones filtradas edad etiqueta

    let PublicacionesFiltradasTipo
     tipo = //filtrar por tipo
        match tipo with
        |"f" -> PublicacionesFiltradasEtiqueta |> List.filter   (fun  x -> x.[3] = tipo  )  // filta por tipo
        |"p" -> PublicacionesFiltradasEtiqueta |> List.filter   (fun  x -> x.[3] = tipo  )  // filta por tipo
        |_ -> PublicacionesFiltradasEtiqueta

    let PublicacionesFiltradasTipoOK = PublicacionesFiltradasTipo tipo

    let fechaBooleanAntes antesDe fecha = // Boolean Antes de lo utiliza filtrar por fecha
        if System.DateTime.Compare (System.DateTime.ParseExact (antesDe, "d/M/yyyy", CultureInfo.InvariantCulture ), System.DateTime.ParseExact (fecha, "d/M/yyyy", CultureInfo.InvariantCulture )) = 1 then
         true
        else false

    let fechaBooleanDespues despuesDe fecha = // Boolean Antes de lo utiliza filtrar por fecha
        if System.DateTime.Compare (System.DateTime.ParseExact (despuesDe, "d/M/yyyy", CultureInfo.InvariantCulture ), System.DateTime.ParseExact (fecha, "d/M/yyyy", CultureInfo.InvariantCulture )) = -1 then
         true
        else false

    let PublicacioneFiltrarFecha (PublicacionesFiltradasTipoOK:string [] list) antesDe despuesDe = // Filtrar por fecha
       match antesDe with
       | "-" -> match despuesDe with
              | "-" -> PublicacionesFiltradasTipoOK
              |_ -> PublicacionesFiltradasTipoOK |> List.filter (fun  x ->  fechaBooleanDespues despuesDe x.[4]   )
       |_ -> match despuesDe with
              | "-" ->  PublicacionesFiltradasTipoOK |> List.filter (fun  x ->  fechaBooleanAntes antesDe x.[4]   )
              |_ ->  PublicacionesFiltradasTipoOK |> List.filter (fun  x -> fechaBooleanAntes antesDe x.[4]  && fechaBooleanDespues despuesDe x.[4]   )

    let PublicacionesFiltradasFechaTipoEtiqueta = PublicacioneFiltrarFecha PublicacionesFiltradasTipoOK realizadaAntes realizadaDespues //juntar filtros

    let PublicacionesFinal1 (PublicacionesFiltradasFechaTipoEtiqueta:string [] list) (NoContieneA:string[]) (contieneA:string[]) = // Filtrar Contieene o no contiene
        match NoContieneA with
        |[||] -> match contieneA with
                |[||] -> PublicacionesFiltradasFechaTipoEtiqueta
                |_ ->  PublicacionesFiltradasFechaTipoEtiqueta |> List.filter (fun x -> EntregoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) contieneA  0 0 0) 
        |_ -> match contieneA with
                |[||] ->PublicacionesFiltradasFechaTipoEtiqueta |> List.filter (fun x -> EntregoNoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) NoContieneA  0 0 0) 
                |_ -> PublicacionesFiltradasFechaTipoEtiqueta |> List.filter (fun x -> EntregoNoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) NoContieneA  0 0 0 && EntregoContieneBoolean (interfaz.GetEtiquetasPublicacion( x.[6] )) contieneA  0 0 0) 

    let PublicacionesFinal = PublicacionesFinal1 PublicacionesFiltradasFechaTipoEtiqueta NoContieneA contieneA // Publicaciones Filtradas 

    let ListaEtiquetasPublicaciones= publication 6 PublicacionesFinal // lisita con todas las etiquetas de las publicaciones

    let HumanosEtiquetadosPorPost = 
        ListaEtiquetasPublicaciones
        |>List.map (fun x -> interfaz.GetEtiquetasPublicacion(x))
    

    let losHumanosEtiquetados= // lista con todos los humanos etiquetados
        HumanosEtiquetadosPorPost
        |> Array.concat
        |> Seq.distinct
        |> Seq.toList

    let grouped = //Agrupa todas las publicaciones echas por un mismo Humano
        losHumanosEtiquetados
        |> Seq.groupBy  (id)
        |> Seq.toList
        |> List.sortBy(fun (_, s) -> Seq.length s)
        |> List.rev

    let humanID =   losHumanosEtiquetados 

    let rec EstaEtiquetado (etiquetado:string []) (esta:string) x = // Ver si esta etiquetado
            if x < etiquetado.Length then
                if etiquetado.[x] <> "-" then
                     if  etiquetado.[x] = esta  then
                         true
                      else
                        EstaEtiquetado etiquetado esta  (x+1)
                else
                    false
            else
                false 

    let tupla2 (ids:string[]) (info:string[][]) = 
        ids 
        |> Seq.map (fun id -> id , info |>Array.filter (fun elem -> EstaEtiquetado (interfaz.GetEtiquetasPublicacion( elem.[6] )) id 0 ) |>Array.toList |> DatoPublicacion 0 0  |> List.toArray ) 
        |> Seq.toArray
        |> Array.sortBy(fun (_, s) -> Seq.length s)
        |> Array.rev

    let final = tupla2 (humanID |>List.toArray) (PublicacionesFinal |>List.toArray) // entrega la tupla final

    let RespuestaConsulta2 = interfaz.EntregarSolucionConsulta2(final)
    printfn "consulta 2"

if consulta = 3 then
    let restriccionConsulta3 = interfaz.GetRestriccionesConsulta3()
    let genero= restriccionConsulta3.de_genero // El usuario que publica debe ser del genero especicado.
    let mayoresDe= restriccionConsulta3.mayor_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser mayores o iguales a la edad entregada
    let menoresDe= restriccionConsulta3.menor_de //Tanto la gente etiquetada, como aquel que recibio la publicacion deben ser menores o iguales a la edad entregada
    let noEsSeguidoPor= restriccionConsulta3.no_es_seguido_por //El usuario que publica no puede ser seguido por NINGUNO de los humanos indicados. El arreglo posee los NOMBRES de las personas involucradas.
    
            
    let rec loSigue (SigueA:string) (NoDebeSeguirA:string []) x y = //ver si sigue o no
        if SigueA <> "-" then
            if x < interfaz.GetHumanosSeguidos(SigueA).Length then 
                if y < NoDebeSeguirA.Length then
                    if interfaz.GetHumanosSeguidos(SigueA).[x].[1]= NoDebeSeguirA.[y] then
                        false
                    else
                    loSigue SigueA NoDebeSeguirA x (y+1)
                else 
                    loSigue SigueA NoDebeSeguirA (x+1) (0)
            else
                false
        else
            true
    let humanosFiltradosNoEsseguidopor (noEsSeguidoPor:string []) (HumanosFiltradosGenero:string [] list) = //filtrar los humanos por si siguen o no siguen a alguien
         match noEsSeguidoPor with
        | [||] -> HumanosFiltradosGenero
        |_ -> HumanosFiltradosGenero |> List.filter  (fun  x -> loSigue x.[5] noEsSeguidoPor 0 0 )  


    let humanosFinal = humanosFiltradosNoEsseguidopor noEsSeguidoPor TodosLosHumanos // lista final de humanos

    let wallIDfiltrado = DatoHumano 0 4 humanosFinal // lista con el Wall ID de todos los humanos filtrados por si sigue o no

    let Publicaciones = // Lista con todas las publicaciones de los humanos filtrados
      wallIDfiltrado
      |> List.map (fun publicacion -> interfaz.GetMuroHumano(publicacion) |> Array.toList)
      |> List.concat

    let HumanosFiltradosGenero genero = // filtra las publicaciones por genero del autor
        match genero with
        |"h" -> Publicaciones |> List.filter   (fun  x -> (datoHumano humanosFinal 0 x.[1] 3) = genero  )  // filta por tipo
        |"m" -> Publicaciones |> List.filter   (fun  x -> (datoHumano humanosFinal 0 x.[1] 3) = genero  )  // filta por tipo
        |_ -> Publicaciones

    let PublicacionesFiltradasporGenero= HumanosFiltradosGenero genero

    let PublicacionesFiltradasEdad mayoresDe menoresDe = // Filtrar por edad del autor
       match mayoresDe with
       | -1 -> match menoresDe with
              | -1 -> PublicacionesFiltradasporGenero
              |_ -> PublicacionesFiltradasporGenero |> List.filter (fun x -> int(datoHumano humanosFinal 0 x.[1] 2) <= (int menoresDe) ) 
       |_ -> match menoresDe with
              | -1 ->  PublicacionesFiltradasporGenero |> List.filter (fun  x -> int(datoHumano humanosFinal 0 x.[1] 2) >= (int mayoresDe)    )
              |_ ->  PublicacionesFiltradasporGenero |> List.filter (fun  x -> int(datoHumano humanosFinal 0 x.[1] 2) <= (int menoresDe) && int(datoHumano humanosFinal 0 x.[1] 2) >= (int mayoresDe)     )

    let PublicacionesFinal =  PublicacionesFiltradasEdad mayoresDe menoresDe

    let likes =  //likes de las publicaiones finales
        publication 5 PublicacionesFinal
        |> List.map (fun x -> interfaz.GetLikesPublicacion(x)) |> Array.concat
        

    let rec todosLosLikes (likes:string [] list) x y =
        if  x < likes.Length  then
            if  likes.[x].Length <> 0 then
                if y < likes.[x].Length then
                 let muro = likes.[x] |> Array.toList
                 let rest = todosLosLikes likes x (y+1)
                 List.append muro rest
                else
                todosLosLikes likes (x+1) (0)
            else todosLosLikes likes (x+1) (0)
        else 
            []
    let likesLista = likes

    let grouped = //Agrupa todas los likes hechos por un mismo Humano
        likesLista
        |> Seq.groupBy  (id)
        |> Seq.toList
        |> List.sortBy(fun (_, s) -> Seq.length s)
        |> List.rev

    let humanID =   List.map fst grouped

    let rec revisarLikes (likes:string []) (humanID:string) x =
        if  x < likes.Length then
            if likes.[x] = humanID then
                true
            else revisarLikes likes humanID (x+1)
        else 
            false

    let PublicacionesLikeadasPorX humanID=  //Publicaciones likeadas por X
        PublicacionesFinal
        |> List.filter (fun elem -> (revisarLikes (interfaz.GetLikesPublicacion( elem.[5])) humanID 0 ))
        |> publication  1 
      
    let cantidadDelikes x = (PublicacionesLikeadasPorX x).Length  //cantidad de likes que tiene una publicacion, se utiliza una funcion más adelante
    let publicacionesordenLikes = //Ordenar las publicaciones
        humanID
        |> List.sortBy (fun  x ->  cantidadDelikes x)
        |> List.rev

    let tupla3 (ids:string[]) (info:string[][]) = // Diseña la respuesta... lento
        ids 
        |> Seq.map (fun hola -> hola ,  (PublicacionesLikeadasPorX hola) |> Seq.groupBy id |> Seq.toArray|> Array.sortBy(fun (_, s) -> Seq.length s) |> Array.rev)  
        |> Seq.toArray
        |> Array.map (fun (s,e) -> s, Array.map (fun (s,e) -> s, Seq.length e) e)
        |> Array.rev


    let final = tupla3 (publicacionesordenLikes |> List.toArray) (PublicacionesFinal |>List.toArray)


    let RespuestaConsulta3 = interfaz.EntregarSolucionConsulta3(final)
    printfn "consulta 3"

if consulta =4 then

    let restriccionConsulta4 = interfaz.GetRestriccionesConsulta4()
    let genero= restriccionConsulta4.de_genero
    let mayoresDe= restriccionConsulta4.mayor_de 
    let menoresDe= restriccionConsulta4.menor_de 
    let noEsSeguidoPor= restriccionConsulta4.no_es_seguido_por 
  
    let wallID = DatoHumano 0 4 TodosLosHumanos
    let humanID = DatoHumano 0 0 TodosLosHumanos
    let Publicaciones = // Lista con todas las publicaciones
      wallID
      |> List.map (fun publicacion -> interfaz.GetMuroHumano(publicacion) |> Array.toList)
      |> List.concat


    let likes = //likes por Publicacion
        publication 5 Publicaciones
        |> List.map (fun x -> interfaz.GetLikesPublicacion(x))

    let mapAndFilterOut (f : 'a -> 'b) (shouldGo : 'b -> bool) = List.map (Array.map f >> Array.filter (shouldGo >> not))

    let likesFiltradosGenero genero likes = //filtrar genero autor
        let dato x = string (datoHumano TodosLosHumanos 0 x 3)
        let filtro =
            match genero with
            | "h" -> dato >> ((=) "h")
            | "m" -> dato >> ((<>) "m")
            | g -> failwith ("sin genero: " + g)
        likes 
        |> List.map (Array.filter filtro)

    let LikesFiltradosGenero genero = 
        match genero with
        |"h" -> likesFiltradosGenero genero likes
        |"m" -> likesFiltradosGenero genero likes
        |_ -> likes 
    
    let  LikesFiltradoGenero =  LikesFiltradosGenero genero 
    printfn "4"
   
    



    
if consulta =5  then
    let humanID = DatoHumano 0 0 humanosID
    let seguidosID = DatoHumano 0 5 humanosID
    let humanos =TodosLosHumanos

    let rec NoSigueA humano (humanosQueSigue:string [] []) x = 
        if x < humanosQueSigue.Length then
            if humano=humanosQueSigue.[x].[0] then
                false
            else
             NoSigueA humano humanosQueSigue (x+1)
        else
            true

    let tupla5 (ids:string[] []) (humanosID:string[][]) = 
        ids 
        |> Seq.map (fun id -> id.[0] ,interfaz.GetHumanosSeguidos((id.[5]))  |>Array.filter (fun z -> if string(datoHumano humanos 0 z.[0] 5)="-" then true else NoSigueA id.[0] (interfaz.GetHumanosSeguidos(string(datoHumano humanos 0 z.[0] 5)))0 )|> Array.map (fun x-> x.[0]) ) 
        |> Seq.toArray


    let tupla = tupla5 (humanosID |>List.toArray) (humanos|>List.toArray)|> Array.sortBy(fun (_, s) -> Seq.length s) |>Array.rev // Tupla... funciona muy lento en set de datos grandes pero en Chico funciona bien

    let entregarConsulta = interfaz.EntregarSolucionConsulta5(tupla)
    printfn "consulta 5"

if consulta =6 then
    let humanID = DatoHumano 0 0 humanosID
    let seguidosID = DatoHumano 0 5 humanosID
    let humanos =TodosLosHumanos
    let humanosID = [for i in humanos -> i]

    let rec followers (seguido:string) (id:string [] []) x y =
        if y < id.Length then
            if id.[y].[5] <> "-" then
                let hola = interfaz.GetHumanosSeguidos(id.[y].[5])
                if x< hola.Length  then
                        if hola.[x].[0] = seguido then
                                    let muro = [id.[y].[0]]
                                    let rest = followers seguido id (x+1) y
                                    List.append muro rest
                        else
                            followers seguido id (x+1) y
                else
                    followers seguido id (0) (y+1)
             else
                []
        else
            []

    let rec SigueA (sigue:string) (humano:string) y = 
        if sigue <> "-" then
            let hola =interfaz.GetHumanosSeguidos (sigue)
            if y < hola.Length then
                if hola.[y].[0]= humano then
                    false
                else
                SigueA sigue humano (y+1)
            else
                true
        else 
            true
  
    let tupla6 (ids:string[] []) = //tarda mucho en entregar set de datos completos :( pero el chico funciona bien
        ids 
        |> Seq.map (fun id -> id.[0] , (followers id.[0] ids 0 0) |> List.filter (fun y-> SigueA id.[5] y 0 )  |>List.toArray)
        |> Seq.toArray
        |> Array.sortBy(fun (_, s) -> Seq.length s) 
        |> Array.rev


    let final = tupla6 (humanosID |> List.toArray) 
    
    let entregarConsulta = interfaz.EntregarSolucionConsulta6(final)
    printfn "consulta 6"
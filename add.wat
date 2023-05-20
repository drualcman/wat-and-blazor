(module (; Solo un module por fichero ;)
    (func $add 
        (param $A i32) (param $B i32) 
        (result i32)
        
        local.get $A    (; meter en el stack el primer parametro;)
        local.get $B    (; meter en el stack el segundo parametro;)

        (; el stack tiene 2 datos apilados;)

        i32.add     (; esto recupera del stack y todo lo que haya, lo suma y deja en el stack el resultado ;)

        (; en el stack ahra solo hay un dato con el resultado ;)               
    )
    (export "add" (func $add))      (; esto hace que se vea la funcion entre comillas y que se ejecute la funcion junto a las commillas ;)
)

import {useEffect, useState} from "react";
import {todoClient} from "./baseUrl.ts";
import type {CreateTodoDto, Todo} from "./generated-ts-client.ts";


function App() {

    const [todos, setTodos] = useState<Todo[]>([])
    const [myform, setMyform] = useState<CreateTodoDto>({
        description: "",
        priority: 0,
        title: ""
    })

    useEffect(() => {
        todoClient.getAllTodos().then(r => {
            setTodos(r)
        })
    }, [])



  return (
    <>
        <input value={myform.title} onChange={e => setMyform({...myform, title: e.target.value})} placeholder="your title" />
        <input value={myform.description} onChange={e => setMyform({...myform, description: e.target.value})}  placeholder="your discrp" />
        <input value={myform.priority}  onChange={e => setMyform({...myform, priority: Number.parseInt(e.target.value)})} type="number" placeholder="your priority" />

        <button onClick={() => {
            todoClient.createTodo(myform).then(result => {
                console.log("YUPPIE")
                setTodos([...todos, result])
            })

        }}>New TODO</button>


        <hr />



        {
            todos.map(t => {
                return <div key={t.id}>
                    <input type="checkbox" checked={t.isdone} onChange={async() =>{
                        const result = await todoClient.toggleChecked(t);
                        const index = todos.indexOf(t);
                        const duplicate = [...todos];
                        duplicate[index] = result;
                        setTodos(duplicate);
                    }}  />


                    {JSON.stringify(t)}
                </div>
    })
        }
    </>
  )
}

export default App

import {IGameItem} from "../../../interfaces/games";
import {useState, useEffect} from "react";
import http_common from "../../../api/http_common.ts";

const GamePage = (id: number) => {
    const [data, setData] = useState<IGameItem>();

    useEffect( () => {
        fetchGame();
    });

    const fetchGame = () => {
        http_common.get<IGameItem>(`/api/Games/${id}`)
            .then((resp) => {
                setData(resp.data);
            });
    }

    return(
        <>
        </>
    )
}
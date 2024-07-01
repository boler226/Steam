import axios from "axios";

const http_common = axios.create({
    baseURL: "http://localhost:5002",
    headers: {
        "Content-Type": "application/json"
    }
});

export default http_common;
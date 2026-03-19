import Axios from "axios";

const API_URL = 'https://localhost:7266';
const axios = Axios.create({
  baseURL: API_URL,
//   headers: {
//     "Content-Type": "application/json",
//   },
});

export default axios;
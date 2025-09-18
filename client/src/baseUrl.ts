import {TodoClient} from "./generated-ts-client.ts";

const isProduction = import.meta.env.PROD;


const prod = "https://server-wandering-waterfall-4577.fly.dev"
const dev = "http://localhost:5219"


export const finalUrl = isProduction ? prod : dev;

export const todoClient = new TodoClient(finalUrl);
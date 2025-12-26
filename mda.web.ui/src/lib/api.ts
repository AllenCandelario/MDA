import { env } from '../config/env';

const BASE_URL = env.apiUrl;

export async function request<T>(path: string, options: RequestInit & { body?: any } = {}) : Promise<T> {
    const res = await fetch (
        `${BASE_URL}${path}`, {
            ...options,
            headers: {
                "Content-Type": "application/json",
                ...(options.headers ?? {}),
            },
            body: options.body !== undefined ? JSON.stringify(options.body) : undefined
        }
    );

    const contentType = res.headers.get("content-type") ?? "";
    const data = contentType.includes("application/json") ? await res.json() : await res.text();

    if (!res.ok) {
        throw new Error(typeof data === "string" ? data : JSON.stringify(data));
    }

    return data as T;
}
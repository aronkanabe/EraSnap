export interface GeneratedImage {
    id?: string;
    image?: string;
}

export interface Prompt {
    id?: string;
    name?: string;
    image?: string;
}

export interface GenderPrompt {
    prompt?: Prompt
    gender?: string
}


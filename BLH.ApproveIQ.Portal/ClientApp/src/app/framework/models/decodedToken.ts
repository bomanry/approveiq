export type DecodedToken = {
    nameid: string;
    unique_name: string;
    email: string;
    role: string;
    nbf: Date;
    exp: Date;
    iat: Date;
    iss: string;
    aud: string;
}
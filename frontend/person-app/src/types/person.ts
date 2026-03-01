export interface Person {
    id: string
    firstName: string
    lastName: string
    dateOfBirth: string
    age: number
    address: string
}

export interface PagedResult<T> {
    items: T[]
    totalCount: number
}

export interface CreatePersonRequest {
    firstName: string
    lastName: string
    dateOfBirth: string
    address: string
}
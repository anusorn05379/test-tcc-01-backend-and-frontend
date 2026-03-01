import type { Person, CreatePersonRequest, PagedResult } from '../types/person'

const BASE_URL = 'http://localhost:5032/api/persons'

export const personService = {
    async getPaged(page: number, pageSize: number): Promise<PagedResult<Person>> {
        const response = await fetch(`${BASE_URL}?page=${page}&pageSize=${pageSize}`)
        if (!response.ok) {
            throw new Error('Failed to fetch persons')
        }
        return response.json()
    },
    async getById(id: string): Promise<Person> {
        const response = await fetch(`${BASE_URL}/${id}`)
        if (!response.ok) {
            throw new Error(`Failed to fetch person with id ${id}`)
        }
        return response.json()
    },
    async create(data: CreatePersonRequest): Promise<Person> {
        const response = await fetch(BASE_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        })
        if (!response.ok) {
            throw new Error('Failed to create person')
        }
        return response.json()
    }
}

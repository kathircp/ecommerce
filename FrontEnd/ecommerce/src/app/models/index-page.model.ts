export interface IndexPage {
    moduleName: string,
    filterById: string,
    categoryId: number,
    categoryName: string,
    rank: number,
    filters: Filter[]
}
export interface Filter {
    id: number,
    filtername: string,
    optionName: string,
    optionValue: string
}

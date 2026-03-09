export class PaginationResponse<T>
{
  Total: number;
  Page: number;
  PageCount: number;
  Data: T[]
}
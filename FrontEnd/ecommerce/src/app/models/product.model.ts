/*export interface Product{
  id: number;
  title: string;
  price: number;
  category: string;
  description: string;
  image: string;
}*/
export interface Product{
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
  color: string;
  discount : number;
  blouse : boolean;
  newArrival: boolean;
  imageUrl: string;
}

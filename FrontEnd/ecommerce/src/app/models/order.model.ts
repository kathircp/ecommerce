export interface Order {
  userName: string;
  createdAt: string;
  total: number;
  orderStatus: string;
  remarks: string;
  items: any[];
  address: any;
}
export interface OrderItem{
  productId: string;
  quantity: number;
  unitPrice: number;
  productName: string;
  lineTotal : number;
}
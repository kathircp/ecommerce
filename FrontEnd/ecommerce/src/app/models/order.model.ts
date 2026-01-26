export interface Order {
  userName: string;
  createdAt: string;
  total: number;
  orderStatus: string;
  remarks: string;
  items: any[];
  address: any;
  payment: Payment;
}
export interface OrderItem{
  productId: string;
  quantity: number;
  unitPrice: number;
  productName: string;
  lineTotal : number;
}
export interface Payment{
  mode: string;
  status: string;
  transactionId : string;
}
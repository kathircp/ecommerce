export interface Order {
  id: string;
  orderDate: string;
  totalAmount: number;
  status: string;
  items: any[];
  address: any;
}
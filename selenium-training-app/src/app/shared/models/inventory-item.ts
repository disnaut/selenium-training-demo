export interface InventoryItem {
  id: number;
  sku: string;
  name: string;
  category: string;
  quantity: number;
  status: 'In Stock' | 'Low Stock' | 'Out of Stock';
  location: string;
  lastUpdated: string;
}
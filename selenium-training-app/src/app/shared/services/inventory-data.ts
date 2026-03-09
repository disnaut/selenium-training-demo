import { Injectable } from '@angular/core';
import { Observable, of, delay, map } from 'rxjs';
import { InventoryItem } from '../models/inventory-item';
import { MOCK_INVENTORY_ITEMS } from '../data/mock-inventory-items';

@Injectable({
  providedIn: 'root',
})
export class InventoryData {
  private items: InventoryItem[] = [...MOCK_INVENTORY_ITEMS];

  getInventoryItems(): Observable<InventoryItem[]> {
    return of(this.items).pipe(delay(1200));
  }

  getInventoryItemById(id: number): Observable<InventoryItem | undefined> {
    return of(this.items).pipe(
      delay(800),
      map((items) => items.find((item) => item.id === id)),
    );
  }

  updateInventoryItem(updated: InventoryItem): Observable<InventoryItem> {
    this.items = this.items.map((item) => (item.id === updated.id ? updated : item));
    return of(updated).pipe(delay(1000));
  }
}

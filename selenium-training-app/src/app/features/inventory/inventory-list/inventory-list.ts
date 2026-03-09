import { Component, ViewChild, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { InventoryItem } from '../../../shared/models/inventory-item';
import { MOCK_INVENTORY_ITEMS } from '../../../shared/data/mock-inventory-items';
import { InventoryDialog } from '../inventory-dialog/inventory-dialog';

@Component({
  selector: 'app-inventory-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatSnackBarModule,
  ],
  templateUrl: './inventory-list.html',
  styleUrl: './inventory-list.scss',
})
export class InventoryList {
  private readonly dialog = inject(MatDialog);
  private readonly snackBar = inject(MatSnackBar);

  protected readonly isLoading = signal(true);

  protected readonly displayedColumns: string[] = [
    'sku',
    'name',
    'category',
    'quantity',
    'status',
    'location',
    'lastUpdated',
    'actions',
  ];

  protected readonly dataSource = new MatTableDataSource<InventoryItem>([]);

  @ViewChild(MatPaginator)
  set matPaginator(paginator: MatPaginator) {
    if (paginator) {
      this.dataSource.paginator = paginator;
    }
  }

  @ViewChild(MatSort)
  set matSort(sort: MatSort) {
    if (sort) {
      this.dataSource.sort = sort;
    }
  }

  public constructor() {
    this.dataSource.filterPredicate = (item: InventoryItem, filter: string) => {
      const normalizedFilter = filter.trim().toLowerCase();

      return (
        item.sku.toLowerCase().includes(normalizedFilter) ||
        item.name.toLowerCase().includes(normalizedFilter) ||
        item.category.toLowerCase().includes(normalizedFilter) ||
        item.status.toLowerCase().includes(normalizedFilter) ||
        item.location.toLowerCase().includes(normalizedFilter)
      );
    };

    setTimeout(() => {
      this.dataSource.data = MOCK_INVENTORY_ITEMS;
      this.isLoading.set(false);
    }, 1200);
  }

  protected applyFilter(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.dataSource.filter = input.value.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  protected editItem(item: InventoryItem): void {
    const dialogRef = this.dialog.open(InventoryDialog, {
      width: '700px',
      disableClose: true,
      data: { ...item },
    });

    dialogRef.afterClosed().subscribe((updatedItem: InventoryItem | undefined) => {
      if (!updatedItem) {
        return;
      }

      const updatedData = this.dataSource.data.map((existingItem) =>
        existingItem.id === updatedItem.id ? updatedItem : existingItem,
      );

      this.dataSource.data = [...updatedData];

      this.snackBar.open(`Saved changes to ${updatedItem.name}.`, 'Dismiss', {
        duration: 3000,
        horizontalPosition: 'right',
        verticalPosition: 'top',
      });
    });
  }

  protected deleteItem(item: InventoryItem): void {
    console.log('Delete item', item);
  }

  protected getStatusClass(status: InventoryItem['status']): string {
    switch (status) {
      case 'In Stock':
        return 'status-in-stock';
      case 'Low Stock':
        return 'status-low-stock';
      case 'Out of Stock':
        return 'status-out-of-stock';
      default:
        return '';
    }
  }
}

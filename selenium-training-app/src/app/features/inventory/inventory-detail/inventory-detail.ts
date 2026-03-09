import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

import { InventoryItem } from '../../../shared/models/inventory-item';
import { InventoryData } from '../../../shared/services/inventory-data';
import { InventoryDialog } from '../inventory-dialog/inventory-dialog';

@Component({
  selector: 'app-inventory-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatButtonModule,
    MatCardModule,
    MatChipsModule,
    MatExpansionModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatDialogModule,
  ],
  templateUrl: './inventory-detail.html',
  styleUrl: './inventory-detail.scss',
})
export class InventoryDetail {
  private readonly route = inject(ActivatedRoute);
  private readonly inventoryService = inject(InventoryData);
  private readonly dialog = inject(MatDialog);
  private readonly snackBar = inject(MatSnackBar);

  protected readonly isLoading = signal(true);
  protected readonly item = signal<InventoryItem | null>(null);

  public constructor() {
    const idParam = this.route.snapshot.paramMap.get('id');
    const itemId = Number(idParam);

    this.inventoryService.getInventoryItemById(itemId).subscribe({
      next: (foundItem) => {
        this.item.set(foundItem ?? null);
        this.isLoading.set(false);
      },
      error: () => {
        this.item.set(null);
        this.isLoading.set(false);
        this.snackBar.open('Failed to load inventory item.', 'Dismiss', {
          duration: 3000,
        });
      },
    });
  }

  protected editItem(): void {
    const currentItem = this.item();

    if (!currentItem) {
      return;
    }

    const dialogRef = this.dialog.open(InventoryDialog, {
      width: '700px',
      data: { ...currentItem },
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result: InventoryItem | undefined) => {
      if (!result) {
        return;
      }

      this.inventoryService.updateInventoryItem(result).subscribe({
        next: (updatedItem) => {
          this.item.set(updatedItem);

          this.snackBar.open('Item saved successfully.', 'Dismiss', {
            duration: 3000,
            horizontalPosition: 'right',
            verticalPosition: 'top',
          });
        },
        error: () => {
          this.snackBar.open('Failed to save item.', 'Dismiss', {
            duration: 4000,
          });
        },
      });
    });
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

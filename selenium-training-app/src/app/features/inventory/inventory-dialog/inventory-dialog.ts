import { CommonModule } from '@angular/common';
import { Component, Inject, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { InventoryItem } from '../../../shared/models/inventory-item';

@Component({
  selector: 'app-inventory-dialog',
  standalone: true,
  templateUrl: './inventory-dialog.html',
  styleUrl: './inventory-dialog.scss',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
})
export class InventoryDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<InventoryDialog>);

  readonly form;

  constructor(@Inject(MAT_DIALOG_DATA) public data: InventoryItem) {
    this.form = this.fb.group({
      id: [this.data.id],
      sku: [this.data.sku, [Validators.required]],
      name: [this.data.name, [Validators.required, Validators.minLength(3)]],
      category: [this.data.category, [Validators.required]],
      quantity: [this.data.quantity, [Validators.required, Validators.min(0)]],
      status: [this.data.status, [Validators.required]],
      location: [this.data.location, [Validators.required]],
      lastUpdated: [this.data.lastUpdated, [Validators.required]],
    });
  }
  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.dialogRef.close(this.form.getRawValue() as InventoryItem);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}

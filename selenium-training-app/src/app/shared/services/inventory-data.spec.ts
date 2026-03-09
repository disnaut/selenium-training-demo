import { TestBed } from '@angular/core/testing';

import { InventoryData } from './inventory-data';

describe('InventoryData', () => {
  let service: InventoryData;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InventoryData);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

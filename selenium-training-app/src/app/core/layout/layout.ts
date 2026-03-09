import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-layout',
  imports: [
    RouterLink,
    RouterOutlet,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule
  ],
  templateUrl: './layout.html',
  styleUrl: './layout.scss'
})
export class Layout {}
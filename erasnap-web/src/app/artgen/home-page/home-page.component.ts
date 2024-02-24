import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'esp-home-page',
  standalone: true,
  imports: [],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent{
  constructor(private router: Router) { }

  navigate() {
    this.router.navigateByUrl('/generate');
  }
}
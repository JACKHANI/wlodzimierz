import { Component } from '@angular/core';

@Component({
  selector: 'wlodzimierz-nav-top-menu',
  templateUrl: './nav-top-menu.component.html',
  styleUrls: ['./nav-top-menu.component.scss']
})
export class NavTopMenuComponent {

  public isExpanded = true;

  public isTokenVerified(): boolean {
    return false;
  }
}

import { AfterViewInit, Component, ElementRef, ViewChild, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { NavService } from 'src/app/core/services/nav.service';
import { NavItem } from 'src/app/core/viewModels/navItem.viewModel';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements AfterViewInit, OnInit  {
  @ViewChild('appDrawer') appDrawer: ElementRef;
  isExpanded = true;
  display
  navOpen: true;
  navItems: NavItem[] = [
    {
      displayName: 'Service Bulletins',
      iconName: '',
      route: ['devfestfl'],
      onlyAdmin: false,
      expanded:true,
      children: [
        {
          displayName: 'All',
          iconName: '',
          route: ['modules','service-bulletins'],
        },
        {
          displayName: 'By Role',
          iconName: '',
          route: ['modules','service-bulletins','0' ,'bulletinRole'],
        }
      ]
    },
    {
      displayName: 'CMM',
      iconName: '',
      route: ['disney'],
      onlyAdmin: false,
      expanded:true,
      children: [
        {
          displayName: 'All',
          iconName: '',
          route: ['modules','manuals'],
        },
        {
          displayName: 'By Role',
          iconName: '',
          route: ['modules','manuals','0' ,'cmmRole'],
        }
      ]
    },
    {
      displayName: 'Entity',
      iconName: '',
      route: ['disney'],
      onlyAdmin: true,
      expanded:false,
      children: [
        {
          displayName: 'All',
          iconName: '',
          onlyAdmin: true,
          route: ['modules','entities'],
        }
      ]
    },
    {
      displayName: 'Role',
      iconName: '',
      route: ['disney'],
      onlyAdmin: true,
      expanded:false,
      children: [
        {
          displayName: 'All',
          iconName: '',
          onlyAdmin: true,
          route: ['modules','roles'],
        }
      ]
    }
  ];
  isAdmin:boolean;
  constructor(
    private navService: NavService,
    private authService: AuthService) {
  }
  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
  }
  ngAfterViewInit(): void {
    this.navService.appDrawer = this.appDrawer;
  }

  handleOpen(isOpen){
    console.log(isOpen);
    this.navOpen = isOpen;
  }

}

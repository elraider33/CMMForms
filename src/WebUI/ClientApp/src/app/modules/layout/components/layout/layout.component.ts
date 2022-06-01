import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NavService } from 'src/app/core/services/nav.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements AfterViewInit  {
  @ViewChild('appDrawer') appDrawer: ElementRef;
  constructor(private navService: NavService) { }

  ngAfterViewInit(): void {
    this.navService.appDrawer = this.appDrawer;
  }

}

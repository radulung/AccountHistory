import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { AccountHistoryPage } from '../account-history/account-history';
import { PaymentsPage } from '../payments/payments';

@Component({
  selector: 'page-menu',
  templateUrl: 'menu.html',
})
export class MenuPage {

  constructor(public navCtrl: NavController, public navParams: NavParams) {
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad MenuPage');
  }

  public navigateToAccountHistory(): void {
    this.navCtrl.push(AccountHistoryPage);
  }

  public navigateToPayments(): void {
    this.navCtrl.push(PaymentsPage);
  }

}

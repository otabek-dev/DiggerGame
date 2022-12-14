# Игра Digger
![image](https://user-images.githubusercontent.com/92029084/198121312-afe4716d-bdeb-405b-a40f-e2abd76e0a6b.png)
---
*Общие правила и описание интерфейса ICreature*
Каждый класс должен уметь:
- Возвращать имя файла, в котором лежит соответствующая ему картинка (например, "Terrain.png")
- Сообщать приоритет отрисовки. Чем выше приоритет, тем раньше рисуется соответствующий элемент, это важно для анимации.
- Действовать — возвращать направление перемещения и, если объект во что-то превращается на следующем ходу, то результат превращения.
- Разрешать столкновения двух элементов в одной клетке.

### Terrain
 *Сделайте класс Terrain, реализовав ICreature. Сделайте так, чтобы он ничего не делал.*

### Player
*Сделайте класс Player, реализовав ICreature.*

- Сделайте так, чтобы диггер шагал в разные стороны в зависимости от нажатой клавиши со стрелкой. Убедитесь, что диггер не покидает пределы игрового поля.

- Сделайте так, чтобы земля исчезала в тех местах, где прошел диггер.

### Sack
*Сделайте класс Sack, реализовав ICreature. Это будет мешок с золотом.*

- Мешок может лежать на любой другой сущности (диггер, земля, мешок, золото, край карты).
- Если под мешком находится пустое место, он начинает падать.
- Если мешок падает на диггера, диггер умирает, а мешок продолжает падать, пока не приземлится на землю, другой мешок, золото или край карты.
- Диггер не может подобрать мешок, толкнуть его или пройти по нему.

### Gold
*Сделайте класс Gold, реализовав ICreature.*

- Мешок превращается в золото, если он падал дольше одной клетки игрового поля и приземлился на землю, на другой мешок, на золото или на край карты.
- Мешок не превращается в золото, а остаётся мешком, если он падал ровно одну клетку.
- Золото никогда не падает.
- Когда диггер собирает золото, ему начисляется 10 очков.

### Monster
*Сделайте класс Monster, реализовав ICreature. Его поведение должно быть таким:*

- Если на карте нет диггера, монстр стоит на месте.
- Если на карте есть диггер, монстр двигается в его сторону по горизонтали или вертикали. Можете написать поиск кратчайшего пути к диггеру, но это не обязательно.
- Монстр не может ходить сквозь землю или мешки.
- Если после хода монстр и диггер оказались в одной клетке, диггер умирает.
- Если монстр оказывается в клетке с золотом, золото исчезает.
- Мешок может лежать на монстре.
- Падающий на монстра мешок убивает монстра.
- Монстр не должен начинать ходить в клетку, где уже есть другой монстр.
- Если два или более монстров сходили в одну и ту же клетку, они все умирают. Если в этой клетке был диггер — он тоже умирает.

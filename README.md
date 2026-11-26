# Taller: Pathfinding en Unity (AI Navigation)

**Autor:** Eythan Coyago  
**Materia:** Desarrollo de Videojuegos  

Prototipo desarrollado en **Unity (C#)** para aplicar los conceptos de búsqueda de caminos (*pathfinding*) y el algoritmo **A\*** utilizando el paquete oficial `Unity.AI.Navigation`.

## ¿Qué hace el prototipo?

La escena simula una arena de sigilo y persecución dividida en dos rutas (un pasillo corto con terreno difícil y un pasillo largo seguro), además de una plataforma separada para probar saltos:

- **Patrulla y persecución (`GuardAI.cs`):** El enemigo recorre 3 puntos de vigilancia (*waypoints*) y, cuando detecta al jugador dentro de su rango, pasa a perseguirlo actualizando su destino en tiempo real.
- **Zonas con costo de movimiento (`NavMeshModifier`):** El pasillo superior tiene una zona naranja (`ZonaCostosa`) con un costo de **6** frente al costo **1** del piso normal. El algoritmo A\* evalúa este peso y decide rodear por el pasillo inferior por ser más rentable.
- **Obstáculos dinámicos (`NavMeshObstacle` con *Carving*):** Al presionar la tecla `Espacio`, se activa una compuerta que recorta la malla de navegación en vivo y obliga al enemigo a recalcular su ruta al instante por encima de la zona peligrosa.
- **Salto entre plataformas (`NavMeshLink`):** Conecta el suelo principal con una plataforma elevada a la derecha para que el agente pueda cruzar el vacío durante la persecución.
- **Trazado de ruta en pantalla:** Usa un `LineRenderer` para dibujar los vértices (`agent.path.corners`) que calcula A\* y un texto en pantalla (`EstadoText`) con el estado actual de la IA y la distancia restante.

## Controles

- **`W A S D` o Flechas:** Mover al jugador (`Player`).
- **`Espacio`:** Abrir o cerrar la puerta dinámica (`PuertaBloqueo`) para probar el recálculo de ruta.

## Scripts principales (`Assets/Scripts/`)

- **`GuardAI.cs`:** Controla la lógica de patrulla, persecución por distancia, dibujo de la ruta con `LineRenderer` y actualización del texto en pantalla.
- **`DemoController.cs`:** Gestiona el movimiento del jugador con `CharacterController` (usando el nuevo `InputSystem`) y activa/desactiva la puerta con la barra espaciadora.

## Paquetes utilizados

- `com.unity.ai.navigation` (AI Navigation)
- `com.unity.inputsystem` (Input System)
- `com.unity.textmeshpro` (TextMeshPro)

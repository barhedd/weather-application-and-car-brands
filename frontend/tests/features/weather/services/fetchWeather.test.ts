import { fetchWeather } from "@/features/weather/services/fetchWeather"

describe("fetchWeather service", () => {
  beforeEach(() => {
    global.fetch = jest.fn()
  })

  it("debería retornar datos si la ciudad existe", async () => {
    const mockData = {
      city: "Madrid",
      temperature: 25,
      humidity: 50,
      description: "Soleado"
    }

    // @ts-ignore
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => mockData
    })

    const data = await fetchWeather("Madrid")
    expect(data).toEqual(mockData)
  })

  it("debería lanzar error si la ciudad no existe (404)", async () => {
    // @ts-ignore
    fetch.mockResolvedValueOnce({
      ok: false,
      status: 404
    })

    await expect(fetchWeather("CiudadFake")).rejects.toThrow("Ciudad no encontrada")
  })

  it("debería lanzar error si hay problema de API distinto", async () => {
    // @ts-ignore
    fetch.mockResolvedValueOnce({
      ok: false,
      status: 500
    })

    await expect(fetchWeather("Madrid")).rejects.toThrow("Error al obtener los datos del clima")
  })

  it("debería lanzar error de red si falla fetch", async () => {
    // @ts-ignore
    fetch.mockRejectedValueOnce(new TypeError("Failed to fetch"))

    await expect(fetchWeather("Madrid")).rejects.toThrow("Error de red. Verifica tu conexión")
  })
})